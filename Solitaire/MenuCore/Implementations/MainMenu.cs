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

    public override void Display(int pickedOption)
    {
        Console.WriteLine(FiggleFonts.Slant.Render(GameStrings.app_name));
        
        Console.WriteLine($"{GameStrings.createdby} [https://github.com/yatoenough]");

        for (int i = 0; i < Options.Count; i++)
        {
            if(pickedOption == i) Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"{i + 1}. {Options[i].GetLabel()}");
            Console.ResetColor();
        }
        
    }
}