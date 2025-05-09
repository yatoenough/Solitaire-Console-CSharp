using Figgle;
using Solitaire.I18n;
using Solitaire.Menu.Options;
using Solitaire.Menu.Options.LanguageOptions;

namespace Solitaire.Menu;

public class LanguageMenu : IMenu
{
    public List<IMenuOption> Options { get; } = [
        new EnglishMenuOption(),
        new PolishMenuOption()
    ];

    public void Display(int pickedOption)
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