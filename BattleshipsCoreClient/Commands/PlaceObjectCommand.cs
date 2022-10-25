using BattleshipsCore.Game.GameGrid;
using BattleshipsCoreClient.Data;
using BattleshipsCoreClient.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCoreClient.Commands
{
    internal class PlaceObjectCommand : ICommand
    {
        private Vec2 ClickedPosition { get; set; }
        private PlaceableObjectButton SelectedObjectButtonData { get; set; }
        private Tile[,] CurrentGrid { get; set; }
        private List<SelectedObject> SelectedTileGroups { get; set; }
        private TableLayoutPanel ButtonGrid { get; set; }

        private List<(Vec2, Color, TileType)>? PreviouslySelectedTileData { get; set; }
        private int? PreviouslyAddedTileGroupIndex { get; set; }

        public PlaceObjectCommand(
            Vec2 clickedPosition,
            TableLayoutPanel buttonGrid,
            List<SelectedObject> selectedTileGroups,
            PlaceableObjectButton buttonData,
            Tile[,] currentGrid)
        {
            ClickedPosition = clickedPosition;
            ButtonGrid = buttonGrid;
            SelectedTileGroups = selectedTileGroups;
            SelectedObjectButtonData = buttonData;
            CurrentGrid = currentGrid;
        }

        public void Execute()
        {
            var gridSize = new Vec2(CurrentGrid.GetLength(1), CurrentGrid.GetLength(0));
            var selectedTiles = SelectedObjectButtonData.PlaceableObject.HoverTiles(gridSize, ClickedPosition);

            PreviouslySelectedTileData = UpdateTiles(
                selectedTiles,
                SelectedObjectButtonData.PlaceableObject.Type.ToColor(),
                SelectedObjectButtonData.PlaceableObject.Type);

            PreviouslyAddedTileGroupIndex = SelectedTileGroups.Count;
            SelectedTileGroups.Add(new SelectedObject(SelectedObjectButtonData, selectedTiles));

            UpdatePlaceableObjectCount(SelectedObjectButtonData, -1);
        }

        public void Undo()
        {
            foreach (var (tile, oldColor, oldTileType) in PreviouslySelectedTileData!)
            {
                var selectedButton = ButtonGrid.GetControlFromPosition(tile.Y, tile.X);

                selectedButton.BackColor = oldColor;
                CurrentGrid[tile.X, tile.Y].Type = oldTileType;
            }

            SelectedTileGroups.RemoveAt(PreviouslyAddedTileGroupIndex!.Value);
            UpdatePlaceableObjectCount(SelectedObjectButtonData, +1);
        }

        private List<(Vec2, Color, TileType)> UpdateTiles(List<Vec2> tiles, Color newColor, TileType newType)
        {
            var updateData = new List<(Vec2, Color, TileType)>(tiles.Count);

            foreach (var tile in tiles)
            {
                var selectedButton = ButtonGrid.GetControlFromPosition(tile.Y, tile.X);

                updateData.Add((tile, selectedButton.BackColor, CurrentGrid[tile.X, tile.Y].Type));

                selectedButton.BackColor = newColor;
                CurrentGrid[tile.X, tile.Y].Type = newType;
            }

            return updateData;
        }

        private void UpdatePlaceableObjectCount(PlaceableObjectButton placeableObjectButtonData, int increment)
        {
            placeableObjectButtonData.LeftCount += increment;
            placeableObjectButtonData.Button.Text = $"{SelectedObjectButtonData.PlaceableObject.Name} x{SelectedObjectButtonData.LeftCount}";
        }
    }
}
