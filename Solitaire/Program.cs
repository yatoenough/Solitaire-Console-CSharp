using System.Globalization;
using System.Text;
using Solitaire.I18n;
using Solitaire.Menu;

namespace Solitaire;

internal static class Program
{
    
    static Program()
    {
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("pl");
        
        Console.Title = GameStrings.app_name;
        
        Console.OutputEncoding = Encoding.UTF8;
    }
    
    private static void Main()
    {
        var mainMenu = new MainMenu();
        var menuOptionPicker = new MenuOptionPicker(mainMenu.OptionsCount);

        while (true)
        {
            Console.Clear();
            mainMenu.Display(menuOptionPicker.PickedOption);
            
            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                {
                    menuOptionPicker.OnUpArrowPressed();
                    break;
                }
                case ConsoleKey.DownArrow:
                {
                    menuOptionPicker.OnDownArrowPressed();
                    break;
                }
                case ConsoleKey.Enter:
                {
                    mainMenu.Confirm(menuOptionPicker.PickedOption);
                    break;
                }
            }
        }
    }
}