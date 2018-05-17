using System;
using System.Threading;
using static cmdtk.Win32;

namespace cmdtk
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.Error.WriteLine("Please provide a command to execute.");
                return 1;
            }

            switch (args[0].ToLower())
            {
                case "opacity": return SetOpacity(args);

                default:
                    Console.Error.WriteLine($"Unknown command: {args[0]}");
                    return 1;
            }
        }

        private static void ShowHelp()
        {
            Console.WriteLine("Usage: cmdtk [command]");
            Console.WriteLine();
            Console.WriteLine("Commands:");
            Console.WriteLine(" opacity [value:0-100]");
        }

        private static IntPtr GetConsoleWindowHandle()
        {
            var oldTitle = Console.Title;
            var newTitle = Guid.NewGuid().ToString();

            Console.Title = newTitle;
            Thread.Sleep(50);

            var handle = FindWindow(IntPtr.Zero, newTitle);

            Console.Title = oldTitle;

            return handle;
        }

        private static int SetOpacity(string[] args)
        {
            if (args.Length < 2 || !int.TryParse(args[1], out var opacity))
            {
                Console.Error.WriteLine("Please provide an opacity value from 0 to 100 percent.");
                return 1;
            }

            var handle = GetConsoleWindowHandle();

            var windowLong = GetWindowLong(handle, GWL_EXSTYLE);

            if ((windowLong & WS_EX_LAYERED) != WS_EX_LAYERED)
                SetWindowLong(handle, GWL_EXSTYLE, windowLong ^ WS_EX_LAYERED);

            SetLayeredWindowAttributes(handle, 0, (byte)(255 * (opacity / 100.0f)), LWA_ALPHA);

            return 0;
        }
    }
}
