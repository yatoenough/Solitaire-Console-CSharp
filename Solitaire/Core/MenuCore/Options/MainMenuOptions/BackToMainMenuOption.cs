using Solitaire.Core.MenuCore.Implementations;
using Solitaire.I18n;

namespace Solitaire.Core.MenuCore.Options.MainMenuOptions;

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