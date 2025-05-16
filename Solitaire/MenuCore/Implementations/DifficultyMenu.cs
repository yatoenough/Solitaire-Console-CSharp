using Figgle;
using Solitaire.I18n;
using Solitaire.MenuCore.Options;
using Solitaire.MenuCore.Options.DifficultyOptions;

namespace Solitaire.MenuCore.Implementations;

public class DifficultyMenu : Menu
{
    public override List<MenuOption> Options { get; } = [
        new EasyDifficultyMenuOption(),
        new HardDifficultyMenuOption()
    ];
    
    public override void Display(int pickedOption)
    {
        Console.WriteLine(FiggleFonts.Standard.Render(GameStrings.difficulty_label));
        
        DisplayOptions(pickedOption);
    }
}