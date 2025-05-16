using Solitaire.I18n;
using Solitaire.MenuCore.Options;
using Solitaire.MenuCore.Options.MainMenuOptions;

namespace Solitaire.MenuCore.Implementations;

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