using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetro
{
    public sealed class Grid
    {
        public readonly int Width;
        public readonly int Height;
        public Dictionary<Point, int> Tiles = new Dictionary<Point, int>();

    }
}
