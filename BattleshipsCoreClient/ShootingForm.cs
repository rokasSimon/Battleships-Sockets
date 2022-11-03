using BattleshipsCore.Data;
using BattleshipsCore.Game;
using BattleshipsCore.Game.GameGrid;
using BattleshipsCore.Game.ShootingStrategy;
using BattleshipsCore.Requests;
using BattleshipsCore.Responses;
using BattleshipsCoreClient.Data;
using BattleshipsCoreClient.Extensions;
using BattleshipsCoreClient.Observer;

namespace BattleshipsCoreClient
{
    public partial class ShootingForm : Form, ISubscriber
    {
        //private GameMapData? OriginalMapData { get; set; }
        private Tile[,]? CurrentGrid { get; set; }
        private bool InputDisabled { get; set; }

        List<SaveTileState> states = new List<SaveTileState>();
        private ShootingStrategy shootingStrategy { get; set; }

        public ShootingForm()
        {
            InputDisabled = true;

            InitializeComponent();

            FormClosed += ShootingForm_FormClosed;

            shootingStrategy = new SingleTileShooting();
            label1.Text = "Active shooting strategy: ";
            label2.Text = " - SingleTileShooting";
        }

        private void ShootingForm_FormClosed(object? sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Initialize(GameMapData opponentMap)
        {
            //OriginalMapData = opponentMap;
            CurrentGrid = opponentMap.Grid;

            var rows = CurrentGrid.GetLength(0);
            var columns = CurrentGrid.GetLength(1);

            TileGrid.ColumnCount = columns;
            TileGrid.RowCount = rows;

            TileGrid.Controls.Clear();
            TileGrid.ColumnStyles.Clear();
            TileGrid.RowStyles.Clear();

            for (int i = 0; i < columns; i++)
            {
                TileGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / columns));
            }

            for (int i = 0; i < rows; i++)
            {
                TileGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / rows));
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    var tile = CurrentGrid[i, j];
                    var button = new Button();

                    button.Name = $"{i}_{j}";
                    //button.BackColor = tile.Type.ToColor();
                    button.BackColor = Color.FromArgb(180, 218, 165, 32);
                    button.Dock = DockStyle.Fill;
                    button.Padding = new Padding(0);
                    button.Margin = new Padding(0);
                    button.Image = new Bitmap(20, 20);
                    button.Click += Button_Click;
                    button.MouseDown += Button_MouseRightClick;

                    TileGrid.Controls.Add(button, j, i);
                    if (tile.Type == TileType.Water || tile.Type == TileType.Ship || tile.Type == TileType.Tank)
                    {
                        var specificButton = new WaterDecorator(button);
                    }
                    if (tile.Type == TileType.Grass)
                    {
                        var specificButton = new GrassDecorator(button);
                    }
                }
            }
        }

        private async void Button_Click(object? sender, EventArgs e)
        {
            if (InputDisabled) return;

            var button = (Button)sender!;
            var coordinates = button!.Name.Split('_');

            int i = int.Parse(coordinates[0]);
            int j = int.Parse(coordinates[1]);
            var pos = new Vec2(i, j);
            

            var targetPositions = shootingStrategy.TargetPositions(pos);

            await GameClientManager.Instance.Client!
                .SendMessageAsync(
                new ShootRequest(GameClientManager.Instance.PlayerName!, targetPositions));
        }
        private async void Button_MouseRightClick(object? sender, MouseEventArgs e)
        {
            //MessageBox.Show("Right click");
            if (InputDisabled) return;

            var button = (Button)sender!;
            var coordinates = button!.Name.Split('_');

            int x = int.Parse(coordinates[0]);
            int y = int.Parse(coordinates[1]);
            var pos = new Vec2(x, y);
            SaveTileState shoot = new SaveTileState(x, y, "marked to shoot");
            SaveTileState shoot2 = new SaveTileState(x, y, "marked to suspect ship");
            SaveTileState shoot3 = new SaveTileState(x, y, "marked to suspect tank");


            if (!states.Any(x => x.x.Equals(x)) && !states.Any(x => x.y.Equals(y)) && e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                MessageBox.Show("Decorator: Marked to shoot ! ");
                var specificButton = new MarkedToShoot(button);

                specificButton.Name = $"{y}_{x}";

                specificButton.Click += Button_Click;
                specificButton.MouseDown += Button_MouseRightClick;
                states.Add(shoot);
            }
            else if (states.Contains(new SaveTileState(x, y, "marked to shoot")) && e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                MessageBox.Show("Decorator: Marked to suspect ship ! ");

                var specificButtonShip = new SuspectShip(button);

                specificButtonShip.Name = $"{y}_{x}";

                specificButtonShip.Click += Button_Click;
                specificButtonShip.MouseDown += Button_MouseRightClick;
                int index = states.FindIndex(s => s.Equals(shoot));

                if (index != -1)
                    states[index] = shoot2;
            }
            else if (states.Contains(new SaveTileState(x, y, "marked to suspect ship")) && e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                MessageBox.Show("Decorator: Marked to suspect tank ! ");

                var specificButtonTank = new SuspectTank(button);

                specificButtonTank.Name = $"{y}_{x}";

                specificButtonTank.Click += Button_Click;
                specificButtonTank.MouseDown += Button_MouseRightClick;
                int index = states.FindIndex(s => s.Equals(shoot2));

                if (index != -1)
                    states[index] = shoot3;
            }
            else if (states.Contains(new SaveTileState(x, y, "marked to suspect tank")) && e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                MessageBox.Show("Decorator: Market to shoot ! ");

                var specificButton = new MarkedToShoot(button);

                specificButton.Name = $"{y}_{x}";

                specificButton.Click += Button_Click;
                specificButton.MouseDown += Button_MouseRightClick;
                int index = states.FindIndex(s => s.Equals(shoot3));

                if (index != -1)
                    states[index] = shoot;
            }
        }

        private async void UpdateGame(List<TileUpdate> updates, GameState newGameState)
        {
            switch (newGameState)
            {
                case GameState.Won: await WinAsync(); break;
                case GameState.Lost: await LoseAsync(); break;
                case GameState.YourTurn: GrantTurn(); break;
                case GameState.EnemyTurn:
                    {
                        foreach (var tu in updates)
                        {
                            UpdateTile(tu);
                        }
                        TakeAwayTurn();
                    }
                    break;
                default: await QuitGameAsync(); return;
            }
        }

        private void UpdateTile(TileUpdate update)
        {
            var tiles = new List<Vec2> { update.TilePosition };

            SetTiles(tiles, update.NewType);
            ColorTiles(tiles, update.NewType);
        }

        private void ColorTiles(List<Vec2> tiles, TileType type)
        {
            foreach (var item in tiles)
            {
                Button selBut = (Button)TileGrid.GetControlFromPosition(item.Y, item.X);//as Button;

                if (type == TileType.Hit)
                {
                    var buttom = new ShootMarkDecorator(selBut);
                }
                if (type == TileType.Miss)
                {
                    var buttom = new MissMarkDecorator(selBut);
                }
            }
        }

        private void SetTiles(List<Vec2> tiles, TileType newType)
        {
            foreach (var tile in tiles)
            {
                if (CurrentGrid == null) return;

                CurrentGrid![tile.X, tile.Y].Type = newType;
            }
        }

        private async Task WinAsync()
        {
            ClearData();
            MessageBox.Show("You won!", "Game Over");
            Facade.PlacementForm = new PlacementForm(2);
            await GameClientManager.Instance.LeaveSessionAsync();
            await Facade.LeaveShootingForm();
        }

        private async Task LoseAsync()
        {
            ClearData();
            MessageBox.Show("You lost!", "Game Over");
            Facade.PlacementForm = new PlacementForm(2);
            await GameClientManager.Instance.LeaveSessionAsync();
            await Facade.LeaveShootingForm();
        }

        private async Task QuitGameAsync()
        {
            ClearData();
            MessageBox.Show("Critical error occured - disconnecting.", "Error");

            await GameClientManager.Instance.DisconnectAsync();
            await Facade.LeaveShootingForm();
        }

        private void GrantTurn()
        {
            const string YourTurnText = "Your Turn";

            InputDisabled = false;
            TurnLabel.Text = YourTurnText;
        }

        private void TakeAwayTurn()
        {
            const string EnemyTurnText = "Enemy Turn";

            InputDisabled = true;
            TurnLabel.Text = EnemyTurnText;
        }

        private void ClearData()
        {
            CurrentGrid = null;
            InputDisabled = true;
        }

        private void SetSingleTileShootingStrategy(object sender, EventArgs e)
        {
            shootingStrategy = new SingleTileShooting();
            label2.Text = " - SingleTileShooting";
        }

        private void SetAreaShootingStrategy(object sender, EventArgs e)
        {
            shootingStrategy = new AreaShooting();
            label2.Text = " - AreaShooting";
        }

        private void SetHorizontalShootingStrategy(object sender, EventArgs e)
        {
            shootingStrategy = new HorizontalLineShooting();
            label2.Text = " - HorizontalLineShooting";
        }

        private void SetVerticalShootingStrategy(object sender, EventArgs e)
        {
            shootingStrategy = new VerticalLineShooting();
            label2.Text = " - VerticalLineShooting";
        }

        public async Task UpdateAsync(BattleshipsCore.Interfaces.Message message)
        {
            if (message is LeftSessionResponse)
            {
                GameClientManager.Instance.ActiveSession = null;

                await Facade.LeaveShootingForm();
            }
            else if (message is DisconnectResponse dr)
            {
                await Facade.SwitchToConnectionFormFrom(this);
            }
            else if (message is SendTileUpdateResponse stur)
            {
                TileGrid.Invoke(() =>
                {
                    UpdateGame(stur.TileUpdate, stur.GameState);
                });
            }
            else if (message is SendMapDataResponse smdr)
            {
                TileGrid.Invoke(() =>
                {
                    Initialize(smdr.MapData);
                });
            }
        }
    }
}
