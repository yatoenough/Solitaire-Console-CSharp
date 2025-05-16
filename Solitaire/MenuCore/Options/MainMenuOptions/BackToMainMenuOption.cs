using Solitaire.I18n;
using Solitaire.MenuCore.Implementations;

namespace Solitaire.MenuCore.Options.MainMenuOptions;

public class BackToMainMenuOption : MenuOption
{
    public override string GetLabel()
    {
        return GameStrings.option_back;
    }

    public override void Execute()
    {
        Menu.Handle(new MainMenu());
    }
}