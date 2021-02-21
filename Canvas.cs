using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetro
{
    public sealed class Canvas
    {
        public readonly Graphics Graphics;
        public readonly Rectangle ClientArea;
        public readonly Font Font;

        public Canvas(Graphics graphics, Rectangle clientArea,Font font)
        {
            this.Graphics = graphics;
            this.ClientArea = clientArea;
            this.Font = font;
        }
    }
}
