using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace AutoClicker_WPF
{
    public class AutoClickerHelper
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;

        public static void ClickMouse(int x, int y, int interval)
        {
            SetCursorPos(x, y);

            mouse_event(MOUSEEVENTF_LEFTDOWN, x, y, 0, 0);
            Thread.Sleep(10); 
            mouse_event(MOUSEEVENTF_LEFTUP, x, y, 0, 0);

            // Ждем интервал перед следующим кликом
            Thread.Sleep(interval);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void SetCursorPos(int x, int y);
    }
}

