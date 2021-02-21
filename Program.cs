using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetro
{
    static class Program
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct Message
        {
            public IntPtr hWnd;
            public int msg;
            public IntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public System.Drawing.Point p;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
                    LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [System.Security.SuppressUnmanagedCodeSecurity] // We won't use this maliciously
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern bool PeekMessage(out Message msg, IntPtr hWnd, uint messageFilterMin, uint messageFilterMax, uint flags);

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static DateTime LastTick;
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private static LowLevelKeyboardProc KeyboardHookCallback = Program.HookCallback;
        private static IntPtr KeyboardHookId = IntPtr.Zero;
        private static formMain MainForm;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Idle += new EventHandler(Program.OnApplicationIdle);
            Program.KeyboardHookId = Program.SetHook(Program.KeyboardHookCallback);
            Program.MainForm = new formMain();
            Application.Run(Program.MainForm);
            Program.UnhookWindowsHookEx(Program.KeyboardHookId);
        }

        private static void OnApplicationIdle(object sender, EventArgs e)
        {
            while (Program.AppStillIdle)
            {
                var currentTime = DateTime.Now;
                var timePassed = (currentTime - Program.LastTick).TotalSeconds;

                foreach (Form form in Application.OpenForms)
                {
                    if (form is formMain && !form.IsDisposed) ((formMain)form).Tick(timePassed);           
                }
            }
        }

        private static bool AppStillIdle
        {
            get
            {
                Message msg;
                return !Program.PeekMessage(out msg, IntPtr.Zero, 0, 0, 0);
            }
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return Program.SetWindowsHookEx(Program.WH_KEYBOARD_LL, proc, Program.GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)Program.WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                if (Program.MainForm != null && !Program.MainForm.IsDisposed) Program.MainForm.KeyboardInput.Input((Keys)vkCode);
            }
            return Program.CallNextHookEx(Program.KeyboardHookId, nCode, wParam, lParam);
        }
    }
}
