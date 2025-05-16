using Solitaire.I18n;
using Solitaire.MenuCore.Implementations;

namespace Solitaire.MenuCore.Options.MainMenuOptions;

public class LanguageMenuOption : MenuOption
{
    public override string GetLabel()
    {
        return GameStrings.language_label;
    }

    public override void Execute()
    {
        Menu.Handle(new LanguageMenu());
    }
}