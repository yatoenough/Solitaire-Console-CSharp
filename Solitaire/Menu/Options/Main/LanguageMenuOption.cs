using Solitaire.I18n;

namespace Solitaire.Menu.Options.Main;

public class LanguageMenuOption : IMenuOption
{
    public string GetLabel()
    {
        return GameStrings.language_label;
    }

    public void Execute()
    {
        var languageMenu = new LanguageMenu();
        var menuOptionPicker = new MenuOptionPicker(languageMenu.OptionsCount);

        while (true)
        {
            Console.Clear();
            languageMenu.Display(menuOptionPicker.PickedOption);
            
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
                    languageMenu.Confirm(menuOptionPicker.PickedOption);
                    return;
                }
            }
        }
    }
}