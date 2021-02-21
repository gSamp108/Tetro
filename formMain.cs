using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetro
{
    public partial class formMain : Form
    {
        public KeyboardInputManager KeyboardInput = new KeyboardInputManager();
        private EngineInterface engine;

        public formMain()
        {
            this.InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        public void Tick(double time)
        {
            var keyboardInput = this.KeyboardInput.GetQueue();
            if (this.engine != null) this.engine.Tick(time, keyboardInput);
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var canvas = new Canvas(e.Graphics, this.ClientRectangle, this.Font);
            if (this.engine != null) this.engine.Render(canvas);
        }
    }
}
