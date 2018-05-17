using System;
using System.Runtime.InteropServices;

namespace cmdtk
{
    public static class Win32
    {
        public const int GWL_EXSTYLE = -20;
        
        public const int LWA_ALPHA = 0x2;
        public const int LWA_COLORKEY = 0x1;

        public const int WS_EX_LAYERED = 0x80000;

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(IntPtr lpClassName, string lpWindowName);        

        [DllImport("user32.dll")]
        public static extern long GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        [DllImport("user32.dll")]
        public static extern long SetWindowLong(IntPtr hWnd, int nIndex, long dwNewLong);
    }
}