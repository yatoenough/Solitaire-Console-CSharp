using Figgle;
using Solitaire.I18n;
using Solitaire.Menu.Options;
using Solitaire.Menu.Options.DifficultyOptions;

namespace Solitaire.Menu.Implementations;

public class DifficultyMenu : IMenu
{
    public List<IMenuOption> Options { get; } = [
        new EasyDifficultyMenuOption(),
        new HardDifficultyMenuOption()
    ];
    
    public void Display(int pickedOption)
    {
        Console.WriteLine(FiggleFonts.Standard.Render(GameStrings.difficulty_label));
        
        for (int i = 0; i < Options.Count; i++)
        {
            if(pickedOption == i) Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"{i + 1}. {Options[i].GetLabel()}");
            Console.ResetColor();
        }
    }
}