using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetro.TetroGame
{
    public sealed class Grid
    {
        public readonly int Width = 10;
        public readonly int Height = 20;
        public Dictionary<Point, int> Tiles = new Dictionary<Point, int>();

        public Grid()
        {
            for (int x = 0; x < this.Width; x++)
            {
                for (int y = 0; y < this.Height; y++)
                {
                    this.Tiles.Add(new Point(x, y), 0);
                }
            }
        }
    }
}
