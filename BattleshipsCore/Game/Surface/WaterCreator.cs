using BattleshipsCore.Game.GameGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Data.Surface
{
    public class WaterCreator : Creator
    {
        public override Tile[,] FactoryMethod(Vec2 size, Tile[,] arr)
        {
            Tile[,] tiles = arr;
            for (int i = 0; i < size.X; i++)
            {
                for (int j = 0; j < size.Y; j++)
                {
                    if (tiles[i,j] == null)
                    {
                        tiles[i, j] = new Water(TileType.Water);
                    }
                }
            }
            return tiles;
        }
    }
}
