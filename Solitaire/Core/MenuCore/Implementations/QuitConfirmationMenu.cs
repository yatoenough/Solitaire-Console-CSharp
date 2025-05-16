using Solitaire.Core.MenuCore.Options;
using Solitaire.Core.MenuCore.Options.MainMenuOptions;
using Solitaire.I18n;

namespace Solitaire.Core.MenuCore.Implementations;

public class QuitConfirmationMenu : Menu
{
    protected override List<MenuOption> Options { get; } =
    [
        new QuitMenuOption(),
        new EmptyMenuOption(GameStrings.option_dontquit)
    ];

    protected override void DisplayMenu()
    {
        Console.WriteLine($"{GameStrings.quit_confirmation_label}\n");
    }
}