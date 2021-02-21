using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetro
{
    public sealed class KeyboardInputManager
    {
        private static double minimumKeyboardDelay = 100;
        private Dictionary<Keys, DateTime> lastTimeInputUsed = new Dictionary<Keys, DateTime>();
        private Queue<Keys> inputQueue = new Queue<Keys>();

        public void Input(Keys key)
        {
            var time = DateTime.Now;
            if (!lastTimeInputUsed.ContainsKey(key)) this.SendInput(time,key);
            else
            {
                var lastTime = this.lastTimeInputUsed[key];
                var delta = (time - lastTime).TotalMilliseconds;
                if (delta >= KeyboardInputManager.minimumKeyboardDelay) this.SendInput(time, key);
            }
        }

        private void SendInput(DateTime time, Keys key)
        {
            if (!this.lastTimeInputUsed.ContainsKey(key)) this.lastTimeInputUsed.Add(key, DateTime.Now);
            this.lastTimeInputUsed[key] = time;
            this.inputQueue.Enqueue(key);
        }

        public Queue<Keys> GetQueue()
        {
            var currentQueue = new Queue<Keys>(this.inputQueue);
            this.inputQueue.Clear();
            return currentQueue;
        }
    }
}
