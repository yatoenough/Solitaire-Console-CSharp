using Solitaire.Core.MenuCore.Implementations;
using Solitaire.I18n;

namespace Solitaire.Core.MenuCore.Options.MainMenuOptions;

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