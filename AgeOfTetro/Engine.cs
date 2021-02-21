using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetro.AgeOfTetro
{
    public sealed class Engine : EngineInterface
    {
        private World World;
        private Position CameraPosition;
        private double CameraSpeed = 10;

        public Engine()
        {
            this.World = new World();
        }

        public void Render(Canvas canvas)
        {
        }

        public void Tick(double time, Queue<Keys> keyboardInput)
        {
            this.HandleInput(time, keyboardInput);
            var currentTile = this.World.GetTile(this.CameraPosition);
        }

        private void HandleInput(double time, Queue<Keys> keyboardInput)
        {
            var frameCameraSpeed = (this.CameraSpeed * time);

            while (keyboardInput.Count > 0)
            {
                var key = keyboardInput.Dequeue();
                if (key == Keys.W) this.CameraPosition = this.CameraPosition.MoveFraction(0, -frameCameraSpeed);
                if (key == Keys.D) this.CameraPosition = this.CameraPosition.MoveFraction(frameCameraSpeed, 0);
                if (key == Keys.S) this.CameraPosition = this.CameraPosition.MoveFraction(0, frameCameraSpeed);
                if (key == Keys.A) this.CameraPosition = this.CameraPosition.MoveFraction(-frameCameraSpeed, 0);
            }
        }
    }
}
