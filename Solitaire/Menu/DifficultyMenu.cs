using Figgle;
using Solitaire.I18n;
using Solitaire.Menu.Options;

namespace Solitaire.Menu;

public class DifficultyMenu : Menu
{
    private readonly List<IMenuOption> options = [
        new EasyDifficultyMenuOption(),
        new HardDifficultyMenuOption()
    ];
    
    public new int OptionsCount => options.Count;

    public override void Display(int pickedOption)
    {
        Console.WriteLine(FiggleFonts.Standard.Render(GameStrings.difficulty_label));
        for (int i = 0; i < OptionsCount; i++)
        {
            if(pickedOption == i) Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"{i + 1}. {options[i].GetLabel()}");
            Console.ResetColor();
        }
    }

    public override void Confirm(int pickedOption)
    {
        options[pickedOption].Execute();
    }
}