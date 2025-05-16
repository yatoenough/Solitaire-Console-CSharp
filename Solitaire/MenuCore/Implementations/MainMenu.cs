using Figgle;
using Solitaire.I18n;
using Solitaire.MenuCore.Options;
using Solitaire.MenuCore.Options.MainMenuOptions;

namespace Solitaire.MenuCore.Implementations;

public class MainMenu : Menu
{
    public override List<MenuOption> Options { get; } = [
        new StartGameMenuOption(),
        new LanguageMenuOption(),
        new QuitMenuOption()
    ];

    public override void Display()
    {
        Console.WriteLine(FiggleFonts.Slant.Render(GameStrings.app_name));
        
        Console.WriteLine($"{GameStrings.createdby} [https://github.com/yatoenough]");

        DisplayOptions();
    }
}