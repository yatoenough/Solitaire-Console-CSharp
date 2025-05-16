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
    
    public override void Display()
    {
        Console.WriteLine($"{GameStrings.restart_confirmation_label}\n");
        
        DisplayOptions();
    }
}