using Solitaire.I18n;
using Solitaire.MenuCore.Options;
using Solitaire.MenuCore.Options.MainMenuOptions;

namespace Solitaire.MenuCore.Implementations;

public class RestartMenu : Menu
{
    public override List<MenuOption> Options { get; } =
    [
        new StartGameMenuOption(GameStrings.restart_label),
        new EmptyMenuOption(GameStrings.dontrestart_label)
    ];
    
    public override void Display(int pickedOption)
    {
        Console.WriteLine($"{GameStrings.restart_confirmation_label}\n");
        
        for (int i = 0; i < Options.Count; i++)
        {
            if(pickedOption == i) Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"{i + 1}. {Options[i].GetLabel()}");
            Console.ResetColor();
        }
    }
}