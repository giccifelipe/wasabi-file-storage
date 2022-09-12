namespace Wasabi.File.Helpers
{
    internal static class ConsoleHelper
    {
        public static void WriteError(string message, bool writeLine = true)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;
            if (writeLine)
                Console.WriteLine(message);
            else
                Console.Write(message);
            Console.ResetColor();
        }

        public static void WriteSuccess(string message, bool writeLine = true)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            if (writeLine)
                Console.WriteLine(message);
            else
                Console.Write(message);
            Console.ResetColor();
        }

        public static void WriteWarning(string message, bool writeLine = true)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (writeLine)
                Console.WriteLine(message);
            else
                Console.Write(message);
            Console.ResetColor();
        }

        public static void WriteInfo(string message, bool writeLine = true)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            if (writeLine)
                Console.WriteLine(message);
            else
                Console.Write(message);
            Console.ResetColor();
        }

        private static void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        private static void Write(string message)
        {
            Console.Write(message);
        }
    }
}
