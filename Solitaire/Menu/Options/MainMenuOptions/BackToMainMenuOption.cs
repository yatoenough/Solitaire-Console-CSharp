using Solitaire.I18n;
using Solitaire.Menu.Implementations;

namespace Solitaire.Menu.Options.MainMenuOptions;

public class BackToMainMenuOption : IMenuOption
{
    public string GetLabel()
    {
        return GameStrings.option_back;
    }

    public void Execute()
    {
        IMenu mainMenu = new MainMenu();
        var menuOptionPicker = new MenuOptionPicker(mainMenu.Options.Count);
        
        
        IMenu.HandleSubMenuInteraction(mainMenu, menuOptionPicker);
    }
}