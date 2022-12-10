using BattleshipsCore.Game.GameGrid;
using BattleshipsCoreClient.PlacementFormComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCoreClient.Iterator
{
    public class GameTileIterator : ITileIterator
    {
        private TileGrid _tileGrid;
        int rowIndex = 0;
        int columnIndex = 0;

        public GameTileIterator(TileGrid tileGrid)
        {
            this._tileGrid = tileGrid;
        }

        public Tile CurrentItem()
        {
            return _tileGrid[rowIndex, columnIndex];
        }

        public Tile First()
        {
            rowIndex = 0;
            columnIndex = 0;
            return _tileGrid[rowIndex, columnIndex];
        }

        public bool IsDone()
        {
            return rowIndex >= _tileGrid.RowCount; 
        }

        public Tile? Next()
        {
            if (columnIndex >= _tileGrid.ColumnCount - 1) { 
                rowIndex++;
                columnIndex = 0;
            } else {
                columnIndex++;
            }

            if (!IsDone())
                return _tileGrid[rowIndex, columnIndex];
            else
                return null;
        }

        public List<Tile> getAdjectedTiles()
        {
            var grid = _tileGrid;
            var adjectedTiles = new List<Tile>();
            int n = _tileGrid.RowCount;
            int m = _tileGrid.ColumnCount;
            int i = rowIndex;
            int j = columnIndex;

            if (isValidPos(i - 1, j - 1, n, m))
            { adjectedTiles.Add(grid[i - 1, j - 1]); }
            if (isValidPos(i - 1, j, n, m))
            { adjectedTiles.Add(grid[i - 1, j]); }
            if (isValidPos(i - 1, j + 1, n, m))
            { adjectedTiles.Add(grid[i - 1, j + 1]); }
            if (isValidPos(i, j - 1, n, m))
            { adjectedTiles.Add(grid[i, j - 1]); }
            if (isValidPos(i, j + 1, n, m))
            { adjectedTiles.Add(grid[i, j + 1]); }
            if (isValidPos(i + 1, j - 1, n, m))
            { adjectedTiles.Add(grid[i + 1, j - 1]); }
            if (isValidPos(i + 1, j, n, m))
            { adjectedTiles.Add(grid[i + 1, j]); }
            if (isValidPos(i + 1, j + 1, n, m))
            { adjectedTiles.Add(grid[i + 1, j + 1]); }

            return adjectedTiles;
        }

        public static bool isValidPos(int i, int j, int n, int m)
        {
            if (i < 0 || j < 0 || i > n - 1 || j > m - 1)
                return false;
            return true;
        }
    }
}
