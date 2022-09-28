using BattleshipsCore.Data;
using BattleshipsCore.Game.GameGrid;
using BattleshipsCore.Game.PlaceableObjects;
using BattleshipsCoreClient.Data;
using BattleshipsCoreClient.Extensions;

namespace BattleshipsCoreClient
{
    public partial class PlacementForm : Form
    {
        private GameMapData? MapData { get; set; }
        private Vec2 GridSize => new Vec2(MapData!.Grid.GetLength(1), MapData.Grid.GetLength(0));

        private PlaceableObjectButton? SelectedPlaceableObject { get; set; }
        private Dictionary<Guid, PlaceableObjectButton> PlaceableObjectButtons { get; set; }
        private List<SelectedObject> SelectedTileGroups { get; set; }
        private List<Vec2> HoveredButtonPositions { get; set; }

        private readonly PlaceableObject[] _placeableObjects = new[]
        {
            new Ship("One Tile Ship", 3, 1),
            new Ship("Two Tile Ship", 2, 2),
            new Ship("Three Tile Ship", 1, 3),
            new Ship("Four Tile Ship", 1, 4),
        };

        public PlacementForm()
        {
            HoveredButtonPositions = new List<Vec2>();
            SelectedTileGroups = new List<SelectedObject>();
            PlaceableObjectButtons = new Dictionary<Guid, PlaceableObjectButton>();
            InitializeComponent();

            foreach (var item in _placeableObjects)
            {
                var button = new Button();
                var buttonId = Guid.NewGuid();

                button.Text = $"{item.Name} x{item.MaximumCount}";
                button.Name = buttonId.ToString();
                button.AutoSize = true;
                button.AutoSizeMode = AutoSizeMode.GrowAndShrink;

                button.Click += PlaceableObjectButton_Click;

                PlaceableObjectPanel.Controls.Add(button);
                PlaceableObjectButtons.Add(buttonId, new PlaceableObjectButton(button, item, item.MaximumCount));
            }

            FormClosed += PlacementForm_FormClosed;
        }

        private void PlacementForm_FormClosed(object? sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        public void ShowWindow(GameMapData mapData)
        {
            Program.ActiveSessionForm.Hide();

            MapData = mapData;

            InitializeGrid();
            Show();
        }

        private void InitializeGrid()
        {
            var rows = MapData!.Grid.GetLength(0);
            var columns = MapData!.Grid.GetLength(1);

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
                    var tile = MapData.Grid[i, j];
                    var button = new Button();

                    button.Name = $"{i}_{j}";
                    button.BackColor = tile.Type.ToColor();
                    button.Dock = DockStyle.Fill;
                    button.Padding = new Padding(0);

                    button.MouseHover += Button_MouseHover;
                    button.Click += Button_Click;
                    button.MouseDoubleClick += Button_MouseDoubleClick;

                    TileGrid.Controls.Add(button, j, i);
                }
            }
        }

        private void Button_MouseDoubleClick(object? sender, MouseEventArgs e)
        {
            var button = (Button)sender!;
            var coordinates = button!.Name.Split('_');

            int x = int.Parse(coordinates[0]);
            int y = int.Parse(coordinates[1]);
            var pos = new Vec2(x, y);

            int indexToRemove = -1;
            for (int i = 0; i < SelectedTileGroups.Count; i++)
            {
                if (SelectedTileGroups[i].Tiles.Contains(pos)) indexToRemove = i;
            }

            if (indexToRemove >= 0)
            {
                UpdatePlaceableObjectButtonCount(SelectedTileGroups[indexToRemove].ButtonData, +1);

                RestoreTileColor(SelectedTileGroups[indexToRemove].Tiles);

                SelectedTileGroups.RemoveAt(indexToRemove);
            }
        }

        private void PlaceableObjectButton_Click(object? sender, EventArgs e)
        {
            var button = (Button)sender!;
            var buttonId = Guid.Parse(button.Name);

            var placeableObjectButton = PlaceableObjectButtons[buttonId];

            SelectedPlaceableObject = placeableObjectButton;
        }

        private void Button_MouseHover(object? sender, EventArgs e)
        {
            if (SelectedPlaceableObject == null) return;

            if (HoveredButtonPositions.Count != 0)
            {
                RestoreTileColor(HoveredButtonPositions);
                HoveredButtonPositions.Clear();
            }

            var button = (Button)sender!;
            var coordinates = button!.Name.Split('_');

            int i = int.Parse(coordinates[0]);
            int j = int.Parse(coordinates[1]);
            var pos = new Vec2(i, j);

            if (SelectedPlaceableObject.PlaceableObject.IsPlaceable(MapData!.Grid, pos))
            {
                HoveredButtonPositions = SelectedPlaceableObject.PlaceableObject.HoverTiles(GridSize, pos);
                ColorTiles(HoveredButtonPositions, Color.LightGray);
            }
        }

        private void Button_Click(object? sender, EventArgs e)
        {
            if (SelectedPlaceableObject == null) return;

            HoveredButtonPositions.Clear();
            var placeableObjectButton = SelectedPlaceableObject;
            if (placeableObjectButton.LeftCount < 1) return;

            var button = (Button)sender!;
            var coordinates = button!.Name.Split('_');

            int i = int.Parse(coordinates[0]);
            int j = int.Parse(coordinates[1]);
            var pos = new Vec2(i, j);

            if (placeableObjectButton.PlaceableObject.IsPlaceable(MapData!.Grid, pos))
            {
                var selectedTiles = placeableObjectButton.PlaceableObject.HoverTiles(GridSize, pos);

                ColorTiles(selectedTiles, placeableObjectButton.PlaceableObject.Type.ToColor());

                SelectedTileGroups.Add(new SelectedObject(placeableObjectButton, selectedTiles));
                UpdatePlaceableObjectButtonCount(placeableObjectButton, -1);
            }
        }

        private void LeaveButton_Click(object sender, EventArgs e)
        {
            ClearData();

            GameClientManager.Instance.LeaveSession();

            Program.SessionForm.ShowWindow();
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {

        }

        private void RotateButton_Click(object sender, EventArgs e)
        {
            if (SelectedPlaceableObject == null) return;

            SelectedPlaceableObject.PlaceableObject.Rotate();
        }

        private void ColorTiles(List<Vec2> tiles, Color newColor)
        {
            foreach (var item in tiles)
            {
                var selBut = TileGrid.GetControlFromPosition(item.Y, item.X);

                selBut.BackColor = newColor;
            }
        }

        private void RestoreTileColor(List<Vec2> tiles)
        {
            foreach (var item in tiles)
            {
                var selBut = TileGrid.GetControlFromPosition(item.Y, item.X);

                var originalColor = MapData!.Grid[item.X, item.Y].Type.ToColor();

                selBut.BackColor = originalColor;
            }
        }

        private void UpdatePlaceableObjectButtonCount(PlaceableObjectButton buttonData, int increment)
        {
            buttonData.LeftCount += increment;
            buttonData.Button.Text = $"{buttonData.PlaceableObject.Name} x{buttonData.LeftCount}";
        }

        private void ClearData()
        {
            MapData = null;
            SelectedPlaceableObject = null;
            PlaceableObjectButtons.Clear();
            SelectedTileGroups.Clear();
            HoveredButtonPositions.Clear();
        }
    }
}
