using BattleshipsCore.Game.GameGrid;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Data.Surface
{
    public class GroundCreator : Creator
    {
        public override Tile[,] FactoryMethod(Vec2 size, Tile[,] arr)
        {
            Tile[,] tiles = arr;
            for (int i = 0; i < size.X; i++)
            {
                for (int j = 0; j < size.Y; j++)
                {
                    //if (i < size.X/2 - 4 && i > size.X/2 + 4 && j < size.Y/2 - 4 && j > size.Y/2 + 4)
                    //if(i > 5  && i < 10 && j > 5 && j < 10) i 5-10 j 5-10
                    if (i > 2 && i < 5  && j > 2 && j < 5 )
                    {
                        tiles[i, j] = new Ground(TileType.Ground);
                    }
                    if (i > 10 && i < 12 && j > 10 && j < 12)
                    {
                        tiles[i, j] = new Ground(TileType.Ground);

                    }
                }
            }
            return tiles;
        }

    }
}
