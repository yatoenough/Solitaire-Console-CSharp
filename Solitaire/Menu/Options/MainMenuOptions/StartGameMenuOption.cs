using Solitaire.Config;
using Solitaire.Core;
using Solitaire.I18n;
using Solitaire.Menu.Implementations;

namespace Solitaire.Menu.Options.MainMenuOptions;

public class StartGameMenuOption(string? label = null) : IMenuOption
{
    public string GetLabel()
    {
        return label ?? GameStrings.option_play;
    }

    public void Execute()
    {
        IMenu difficultyMenu = new DifficultyMenu();
        var menuOptionPicker = new MenuOptionPicker(difficultyMenu.Options.Count);

        IMenu.HandleSubMenuInteraction(difficultyMenu, menuOptionPicker, () =>
        {
            var game = new Game(Settings.Difficulty);
            game.Start();
        });
    }
}
