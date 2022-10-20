﻿namespace BattleshipsCore.Server
{
    public class ServerLogger
    {
        private static ServerLogger? _instance;
        private ServerLogger()
        {
            _defaultTextColor = ConsoleColor.Gray;
            _defaultBackgroundColor = ConsoleColor.Black;
        }

        private ConsoleColor _defaultTextColor;
        private ConsoleColor _defaultBackgroundColor;

        public static ServerLogger Instance
        {
            get
            {
                _instance ??= new ServerLogger();

                return _instance;
            }
        }

        public bool ShowTimestamp { get; set; }

        public void LogInfo(string message)
        {
            PrintToConsole(message, _defaultTextColor);
        }

        public void LogWarning(string message)
        {
            PrintToConsole(message, ConsoleColor.Yellow);
        }

        public void LogError(string message)
        {
            PrintToConsole(message, ConsoleColor.Red);
        }

        private void PrintToConsole(string message, ConsoleColor color)
        {
            if (ShowTimestamp) Console.Write($"[{DateTime.Now.ToLongTimeString()}]    ");

            Console.ForegroundColor = color;
            Console.WriteLine(message);

            Console.ForegroundColor = _defaultTextColor;
            Console.BackgroundColor = _defaultBackgroundColor;
        }
    }
}
