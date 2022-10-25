using BattleshipsCore.Data;
using BattleshipsCore.Game;
using BattleshipsCore.Game.GameGrid;
using BattleshipsCore.Game.ShootingStrategy;
using BattleshipsCore.Requests;
using BattleshipsCore.Responses;
using BattleshipsCoreClient.Extensions;
using BattleshipsCoreClient.Observer;

namespace BattleshipsCoreClient
{
    public partial class ShootingForm : Form, ISubscriber
    {
        //private GameMapData? OriginalMapData { get; set; }
        private Tile[,]? CurrentGrid { get; set; }
        private bool InputDisabled { get; set; }

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
                    button.BackColor = tile.Type.ToColor();
                    button.Dock = DockStyle.Fill;
                    button.Padding = new Padding(0);
                    button.Margin = new Padding(0);

                    button.Click += Button_Click;

                    TileGrid.Controls.Add(button, j, i);
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

        private async void UpdateGame(List<TileUpdate> updates, GameState newGameState)
        {
            switch (newGameState)
            {
                case GameState.Won: await WinAsync(); break;
                case GameState.Lost: await LoseAsync(); break;
                case GameState.YourTurn:
                    {
                        foreach (var tu in updates)
                        {
                            Program.PlacementForm.UpdateTile(tu);
                        }
                        GrantTurn();
                    } break;
                case GameState.EnemyTurn:
                    {
                        foreach (var tu in updates)
                        {
                            UpdateTile(tu);
                        }
                        TakeAwayTurn();
                    } break;
                default: await QuitGameAsync(); return;
            }
        }

        private void UpdateTile(TileUpdate update)
        {
            var tiles = new List<Vec2> { update.TilePosition };

            SetTiles(tiles, update.NewType);
            ColorTiles(tiles, update.NewType.ToColor());
        }

        private void ColorTiles(List<Vec2> tiles, Color newColor)
        {
            foreach (var item in tiles)
            {
                var selBut = TileGrid.GetControlFromPosition(item.Y, item.X);

                selBut.BackColor = newColor;
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

            await GameClientManager.Instance.LeaveSessionAsync();
            await Program.LeaveShootingForm();
        }

        private async Task LoseAsync()
        {
            ClearData();
            MessageBox.Show("You lost!", "Game Over");

            await GameClientManager.Instance.LeaveSessionAsync();
            await Program.LeaveShootingForm();
        }

        private async Task QuitGameAsync()
        {
            ClearData();
            MessageBox.Show("Critical error occured - disconnecting.", "Error");

            await GameClientManager.Instance.DisconnectAsync();
            await Program.LeaveShootingForm();
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

        private void SetSingleTileShootingStrategy(object sender, EventArgs e) {
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

        public async Task UpdateAsync()
        {
            var client = GameClientManager.Instance.Client;
            if (client == null) return;

            var message = client.GetMessage();

            if (message is LeftSessionResponse)
            {
                GameClientManager.Instance.ActiveSession = null;

                await Program.LeaveShootingForm();
            }
            else if (message is DisconnectResponse dr)
            {
                await Program.SwitchToConnectionFormFrom(this);
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
