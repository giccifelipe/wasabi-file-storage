using Wasabi.File.Models.Enum;

namespace Wasabi.File.Helpers
{
    internal class MenuHelper
    {
        public static void DisplayMenu()
        {
            ConsoleHelper.WriteInfo("Choose an option for the task you would like to execute:");
            ConsoleHelper.WriteInfo("1: Create bucket.");
            ConsoleHelper.WriteInfo("2: Upload file.");
            ConsoleHelper.WriteInfo("3: Download file.");
            ConsoleHelper.WriteInfo("4: Download files.");
            ConsoleHelper.WriteInfo("5: Delete file.");
            ConsoleHelper.WriteInfo("99: Exit.");
        }

        public static MenuOptions GetMenuOption()
        {
            int opt;
            do
            {
                DisplayMenu();
                var _opt = Console.ReadLine();

                _ = int.TryParse(_opt, out opt);

            } while (!IsMenuValid(opt));

            return (MenuOptions)opt;
        }

        private static bool IsMenuValid(int mOption)
        {
            return Enum.IsDefined(typeof(MenuOptions), mOption);
        }
    }
}
