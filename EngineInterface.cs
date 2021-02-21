using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetro
{
    public interface EngineInterface
    {
        void Tick(double time, Queue<Keys> keyboardInput);
        void Render(Canvas canvas);
    }
}
