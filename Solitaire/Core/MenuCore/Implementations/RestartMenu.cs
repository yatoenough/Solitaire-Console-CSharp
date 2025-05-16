using Solitaire.Core.MenuCore.Options;
using Solitaire.Core.MenuCore.Options.MainMenuOptions;
using Solitaire.I18n;

namespace Solitaire.Core.MenuCore.Implementations;

public class RestartMenu : Menu
{
    protected override List<MenuOption> Options { get; } =
    [
        new StartGameMenuOption(GameStrings.restart_label),
        new EmptyMenuOption(GameStrings.dontrestart_label)
    ];

    protected override void DisplayMenu()
    {
        Console.WriteLine($"{GameStrings.restart_confirmation_label}\n");
    }
}