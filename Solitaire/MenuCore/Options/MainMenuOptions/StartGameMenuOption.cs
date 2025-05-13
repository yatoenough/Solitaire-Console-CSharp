using Solitaire.Config;
using Solitaire.Core;
using Solitaire.I18n;
using Solitaire.MenuCore.Implementations;

namespace Solitaire.MenuCore.Options.MainMenuOptions;

public class StartGameMenuOption(string? label = null) : MenuOption
{
    public override string GetLabel()
    {
        return label ?? GameStrings.option_play;
    }

    public override void Execute()
    {
        Menu difficultyMenu = new DifficultyMenu();
        var menuOptionPicker = new MenuOptionPicker(difficultyMenu.Options.Count);

        Menu.HandleSubMenuInteraction(difficultyMenu, menuOptionPicker, () =>
        {
            var game = new Game(Settings.Difficulty);
            game.Start();
        });
    }
}
