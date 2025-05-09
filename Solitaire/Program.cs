using System.Text;
using Solitaire.I18n;
using Solitaire.Menu;
using Solitaire.Menu.Implementations;

namespace Solitaire;

internal static class Program
{
    
    static Program()
    {
        Console.Title = GameStrings.app_name;
        Console.OutputEncoding = Encoding.UTF8;
    }
    
    private static void Main()
    {
        IMenu mainMenu = new MainMenu();
        var menuOptionPicker = new MenuOptionPicker(mainMenu.Options.Count);

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