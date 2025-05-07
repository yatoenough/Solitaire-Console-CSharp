using Solitaire.Config;
using Solitaire.Core;
using Solitaire.I18n;

namespace Solitaire.Menu.Options.Main;

public class StartGameMenuOption : IMenuOption
{
    public string GetLabel()
    {
        return GameStrings.option_play;
    }

    public void Execute()
    {
        var difficultyMenu = new DifficultyMenu();
        var menuOptionPicker = new MenuOptionPicker(difficultyMenu.OptionsCount);

        while (true)
        {
            Console.Clear();
            difficultyMenu.Display(menuOptionPicker.PickedOption);
            
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
                    difficultyMenu.Confirm(menuOptionPicker.PickedOption);
                    
                    var game = new Game(Settings.Difficulty);
                    game.Start();
                    
                    return;
                }
            }
        }
    }
}
