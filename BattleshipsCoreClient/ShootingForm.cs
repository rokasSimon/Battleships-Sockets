using BattleshipsCore.Data;
using BattleshipsCore.Game.GameGrid;
using BattleshipsCore.Game.ShootingStrategy;
using BattleshipsCore.Requests;
using BattleshipsCore.Responses;
using BattleshipsCoreClient.Extensions;

namespace BattleshipsCoreClient
{
    public partial class ShootingForm : Form
    {
        private GameMapData? OriginalMapData { get; set; }
        private Tile[,]? CurrentGrid { get; set; }
        private bool InputDisabled { get; set; }
        private bool RefreshLoopActive { get; set; }

        private ShootingStrategy shootingStrategy { get; set; }

        public ShootingForm()
        {
            InputDisabled = true;
            RefreshLoopActive = false;

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

        public async Task ShowWindow()
        {
            GetOpponentMap();
            InitialiseGrid();
            Show();

            await EnableRefreshLoop();
        }

        private void InitialiseGrid()
        {
            CurrentGrid = OriginalMapData!.Grid;

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

            var success = await Shoot(pos);

            if (success)
            {
                await TakeAwayTurn();
            }
            else
            {
                MessageBox.Show("Could not shoot tile.", "Error");
            }
        }

        private void GrantTurn()
        {
            const string YourTurnText = "Your Turn";

            InputDisabled = false;
            TurnLabel.Text = YourTurnText;
            //RefreshLoopActive = false;
        }

        private async Task TakeAwayTurn()
        {
            const string EnemyTurnText = "Enemy Turn";

            InputDisabled = true;
            TurnLabel.Text = EnemyTurnText;

            //await EnableRefreshLoop();
        }

        private void GetOpponentMap()
        {
            var opponentMapResponse = GameClientManager.Instance
                .Client!
                .SendCommand<GetOpponentMapRequest, SendMapDataResponse>(
                new GetOpponentMapRequest(GameClientManager.Instance.PlayerName!));

            if (opponentMapResponse == null)
            {
                MessageBox.Show("Could not retrieve opponent map.");
                return;
            }

            OriginalMapData = opponentMapResponse.MapData;
        }

        private async Task EnableRefreshLoop()
        {
            RefreshLoopActive = true;

            while (RefreshLoopActive)
            {
                await Task.Delay(3000); // Just so it doesn't spam requests so much

                if (InputDisabled)
                {
                    var isMyTurnResponse = await GameClientManager.Instance
                        .Client!
                        .SendCommandAsync<GetMyTurnRequest, SendTileUpdateResponse>(
                        new GetMyTurnRequest(GameClientManager.Instance.PlayerName!));

                    // Should not fail unless something bad happened or calling from wrong context
                    if (isMyTurnResponse == null)
                    {
                        QuitGame();
                        return;
                    }

                    if (isMyTurnResponse.GameState == GameState.Won) Win();
                    else if (isMyTurnResponse.GameState == GameState.Lost) Lose();
                    else if (isMyTurnResponse.GameState == GameState.YourTurn)
                    {
                        foreach(var tileUpdate in isMyTurnResponse.TileUpdate)

                        if (tileUpdate != null)
                        {
                            Program.PlacementForm.UpdateTile(tileUpdate);
                        }

                        GrantTurn();
                    }
                    else if (isMyTurnResponse.GameState == GameState.EnemyTurn)
                    {
                        await TakeAwayTurn();
                    }
                    else
                    {
                        QuitGame();
                    }
                }
            }
        }

        private async Task<bool> Shoot(Vec2 position)
        {
            var targetPositions = new List<Vec2>();
            shootingStrategy.targetPositions(targetPositions, position);

            var response = await GameClientManager.Instance
                .Client!
                .SendCommandAsync<ShootRequest, SendTileUpdateResponse>(
                new ShootRequest(GameClientManager.Instance.PlayerName!, targetPositions));

            if (response == null) return false;
            var updated = false;

            foreach (var tileUpdate in response.TileUpdate) {
                if (response.GameState != GameState.Unknown && tileUpdate != null)
                {
                    UpdateTile(tileUpdate);

                    if (response.GameState == GameState.Lost) Lose();
                    else if (response.GameState == GameState.Won) Win();

                    updated = true;
                }
            }

         
            return updated;
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

        private void Win()
        {
            ClearData();
            MessageBox.Show("You won!", "Game Over");

            GameClientManager.Instance.LeaveSession();
            Program.SessionForm.ShowWindow();
        }

        private void Lose()
        {
            ClearData();
            MessageBox.Show("You lost!", "Game Over");

            GameClientManager.Instance.LeaveSession();
            Program.SessionForm.ShowWindow();
        }

        private void QuitGame()
        {
            ClearData();

            GameClientManager.Instance.Disconnect();
            Program.ConnectionForm.ShowWindow();
        }

        private void ClearData()
        {
            OriginalMapData = null;
            CurrentGrid = null;
            InputDisabled = true;
            RefreshLoopActive = false;
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
    }
}
