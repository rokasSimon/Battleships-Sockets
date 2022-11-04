using BattleshipsCore.Game.GameGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Data.Surface
{
    public class GrassCreator : Creator
    {
        public override Tile[,] FactoryMethod(Vec2 size, Tile[,] arr)
        {
            Tile[,] tiles = arr;
            for (int i = 0; i < size.X; i++)
            {
                for (int j = 0; j < size.Y; j++)
                { 
                    if(i > 5  && i < 10 && j > 5 && j < 10)
                    {
                        tiles[i, j] = new Grass(TileType.Grass);
                    }
                }
            }

            return tiles;
        }
    }
}
