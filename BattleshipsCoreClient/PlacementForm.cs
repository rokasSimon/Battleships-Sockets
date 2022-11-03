using BattleshipsCore.Data;
using BattleshipsCore.Game;
using BattleshipsCore.Game.GameGrid;
using BattleshipsCore.Game.PlaceableObjects;
using BattleshipsCore.Requests;
using BattleshipsCore.Responses;
using BattleshipsCore.Server;
using BattleshipsCoreClient.Commands;
using BattleshipsCoreClient.Data;
using BattleshipsCoreClient.Observer;
using BattleshipsCoreClient.PlacementFormComponents;

namespace BattleshipsCoreClient
{
    public partial class PlacementForm : Form, ISubscriber
    {
        private const int MaximumRememberedCommands = 20;

        private TileGrid _tileGrid;
        private PlaceableObjectMenu _placeableObjectMenu;
        private DropoutStack<ICommand> _executedCommandStack;
        private List<PlacedObject> _selectedTileGroups;

        private bool InputDisabled { get; set; }

        private readonly PlaceableObject[] ship1 = new[]
        {           
            new Ship1(1, "Ship A1", 3, 1)                                   
        };
        private readonly PlaceableObject[] ship2 = new[]
        {
            new Ship2(2, "Ship A2", 2, 2)                                   
        };
        private readonly PlaceableObject[] ship3 = new[]
        {
            new Ship3(3, "Ship A3", 1, 3)                                   
        };
        private readonly PlaceableObject[] superShip1 = new[]
        {
            new SuperShip1(1, "Super Ship S1", 8, 4)                                   
        };
        private readonly PlaceableObject[] superShip2 = new[]
        {
            new SuperShip2(4, "Super Ship S2", 2, 5)                                   
        };
        private readonly PlaceableObject[] superShip3 = new[]
        {
            new SuperShip3(5, "Super Ship S3", 4, 6)                                   
        };

        public PlacementForm(int level)
        {
            InitializeComponent();

            InputDisabled = false;
            _executedCommandStack = new DropoutStack<ICommand>(MaximumRememberedCommands);
            _selectedTileGroups = new List<PlacedObject>();

            _placeableObjectMenu = new PlaceableObjectMenu(PlaceableObjectPanel);
            _tileGrid = new TileGrid(TileGrid);
            
            if(level == 1)
            {
                InitializePlaceableObjects(ship1);
                InitializePlaceableObjects(ship2);
                InitializePlaceableObjects(ship3);
            }
            else
            {
                InitializePlaceableObjects(superShip1);
                InitializePlaceableObjects(superShip2);
                InitializePlaceableObjects(superShip3);
            }      
            
            FormClosed += PlacementForm_FormClosed;      
        }

        public void ClearData()
        {
            InputDisabled = false;

            _selectedTileGroups.Clear();
        }

        private void InitializePlaceableObjects(PlaceableObject[] ship)
        {
            foreach (var item in ship)
            {
                _placeableObjectMenu.AddSelection(new PlaceableObjectData(item, item.MaximumCount), PlaceableObjectButton_Click);
            }
        }

        private void InitializeGrid(GameMapData mapData)
        {
            _tileGrid.Initialize(mapData, Button_MouseHover, Button_Click);
        }

        private void PlaceableObjectButton_Click(object? sender, EventArgs e)
        {
            if (InputDisabled) return;

            var button = (Button)sender!;
            var buttonId = Guid.Parse(button.Name);

            var command = new SelectPlaceableObjectCommand(_placeableObjectMenu, buttonId);

            ExecuteCommand(command);

            _executedCommandStack.Push(command);
        }

        private void Button_MouseHover(object? sender, EventArgs e)
        {
            if (InputDisabled || !_placeableObjectMenu.HasSelection) return;

            var button = (Button)sender!;
            var coordinates = button!.Name.Split('_');
            int i = int.Parse(coordinates[0]);
            int j = int.Parse(coordinates[1]);
            var pos = new Vec2(i, j);

            if (_tileGrid.IsPlaceable(pos, _placeableObjectMenu.Selection!))
            {
                var hoverCommand = new HoverTilesCommand(pos, _placeableObjectMenu.Selection!, _tileGrid);

                ExecuteCommand(hoverCommand);

                _executedCommandStack.Push(hoverCommand);
            }
        }

        private void Button_Click(object? sender, EventArgs e)
        {
            if (InputDisabled || !_placeableObjectMenu.CanPlace) return;

            var button = (Button)sender!;
            var coordinates = button!.Name.Split('_');
            int i = int.Parse(coordinates[0]);
            int j = int.Parse(coordinates[1]);
            var pos = new Vec2(i, j);

            if (_tileGrid.IsPlaceable(pos, _placeableObjectMenu.Selection!))
            {
                var placeCommand = new PlaceObjectCommand(
                    pos,
                    _placeableObjectMenu.SelectedButtonId!.Value,
                    _placeableObjectMenu.Selection!,
                    _selectedTileGroups,
                    _tileGrid,
                    _placeableObjectMenu);

                ExecuteCommand(placeCommand);

                _executedCommandStack.Push(placeCommand);
            }
        }

        private async void LeaveButton_Click(object sender, EventArgs e)
        {
            ClearData();

            var session = GameClientManager.Instance.ActiveSession;
            var player = GameClientManager.Instance.PlayerName;

            if (session != null && player != null)
            {
                await GameClientManager.Instance.Client!
                    .SendMessageAsync(new LeaveSessionRequest(session.SessionKey, player));
            }
        }

        private async void PlayButton_Click(object sender, EventArgs e)
        {
            if (InputDisabled) return;

            await GameClientManager.Instance.Client!
                .SendMessageAsync(new StartBattleRequest(GameClientManager.Instance.PlayerName!));
        }

        private void SaveTileButton_Click(object sender, EventArgs e)
        {
            if (InputDisabled || _selectedTileGroups.Count == 0) return;

            var saveTilesCommand = new SaveTilesCommand(
                GameClientManager.Instance.Client!,
                GameClientManager.Instance.PlayerName!,
                _selectedTileGroups);

            ExecuteCommand(saveTilesCommand);

            _executedCommandStack.Push(saveTilesCommand);
        }

        private void RotateButton_Click(object sender, EventArgs e)
        {
            if (InputDisabled || !_placeableObjectMenu.HasSelection) return;

            var rotateCommand = new RotateCommand(_placeableObjectMenu.Selection!);

            ExecuteCommand(rotateCommand);

            _executedCommandStack.Push(rotateCommand);
        }

        private void UndoButton_Click(object sender, EventArgs e)
        {
            if (InputDisabled) return;

            if (_executedCommandStack.Peek() is HoverTilesCommand)
            {
                var hoverCommand = _executedCommandStack.Pop();

                hoverCommand!.Undo();
            }

            var lastCommand = _executedCommandStack.Pop();
            if (lastCommand == null) return;

            lastCommand.Undo();
        }

        private void ExecuteCommand(ICommand command)
        {
            if (_executedCommandStack.Peek() is HoverTilesCommand)
            {
                var hoverCommand = _executedCommandStack.Pop();

                hoverCommand!.Undo();
            }

            command.Execute();
        }

        private void PlacementForm_FormClosed(object? sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        public async Task UpdateAsync(BattleshipsCore.Interfaces.Message message)
        {
            if (message is SendMapDataResponse smdr)
            {
                Invoke(() =>
                {
                    InitializeGrid(smdr.MapData);
                });
            }
            else if (message is SendTileUpdateResponse stur)
            {
                if (stur.GameState == GameState.YourTurn)
                {
                    Invoke(() =>
                    {
                        _tileGrid.SetTiles(stur.TileUpdate);
                    });
                }
            }
            else if (message is StartedBattleResponse sbr)
            {
                InputDisabled = true;

                await Facade.EnableShootingForm();
            }
            else if (message is LeftSessionResponse lsr)
            {
                await Facade.SwitchToSessionListFrom(this);
            }
            else if (message is OkResponse ok)
            {
                Invoke(() =>
                {
                    MessageBox.Show(ok.Text, "Server Message");
                });
            }
        }
    }
}
