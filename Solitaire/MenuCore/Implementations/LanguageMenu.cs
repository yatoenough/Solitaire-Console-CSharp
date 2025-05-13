using Figgle;
using Solitaire.I18n;
using Solitaire.MenuCore.Options;
using Solitaire.MenuCore.Options.LanguageOptions;

namespace Solitaire.MenuCore.Implementations;

public class LanguageMenu : Menu
{
    public override List<MenuOption> Options { get; } = [
        new EnglishMenuOption(),
        new PolishMenuOption()
    ];

    public override void Display(int pickedOption)
    {
        Console.WriteLine(FiggleFonts.Standard.Render(GameStrings.language_label));
        
        for (int i = 0; i < Options.Count; i++)
        {
            if(pickedOption == i) Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"{i + 1}. {Options[i].GetLabel()}");
            Console.ResetColor();
        }
    }
}