using Figgle;
using Solitaire.Core.MenuCore.Options;
using Solitaire.Core.MenuCore.Options.MainMenuOptions;
using Solitaire.I18n;

namespace Solitaire.Core.MenuCore.Implementations;

public class MainMenu : Menu
{
    protected override List<MenuOption> Options { get; } = [
        new StartGameMenuOption(),
        new LanguageMenuOption(),
        new ScoreboardMenuOption(),
        new QuitMenuOption()
    ];

    protected override void DisplayMenu()
    {
        Console.WriteLine(FiggleFonts.Slant.Render(GameStrings.app_name));
        
        Console.WriteLine($"{GameStrings.createdby} [https://github.com/yatoenough]");
    }
}