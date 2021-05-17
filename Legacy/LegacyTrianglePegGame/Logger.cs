using System;

namespace LegacyTrianglePegGame
{
    internal class Logger
    {
        public Logger()
        {
        }

        private static object _MessageLock = new object();

        public static void WriteToScreen(string enterTheRow, bool newLine = false, ConsoleColor? color = null)
        {
            lock (_MessageLock)
            {
                if (color != null)
                    Console.BackgroundColor = (ConsoleColor)color;

                if (newLine)
                {
                    Console.WriteLine(enterTheRow);
                }
                else
                {
                    Console.Write(enterTheRow);
                }
                if (color != null)
                    Console.ResetColor();
            }
        }
    }
}