using Solitaire.I18n;

namespace Solitaire.Menu.Options.MainMenuOptions;

public class LanguageMenuOption : IMenuOption
{
    public string GetLabel()
    {
        return GameStrings.language_label;
    }

    public void Execute()
    {
        IMenu languageMenu = new LanguageMenu();
        var menuOptionPicker = new MenuOptionPicker(languageMenu.Options.Count);

        IMenu.HandleSubMenuInteraction(languageMenu, menuOptionPicker);
    }
}