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
        Menu languageMenu = new LanguageMenu();
        var menuOptionPicker = new MenuOptionPicker(languageMenu.Options.Count);

        Menu.HandleSubMenuInteraction(languageMenu, menuOptionPicker);
    }
}