using Figgle;
using Solitaire.I18n;
using Solitaire.MenuCore.Options;
using Solitaire.MenuCore.Options.DifficultyOptions;

namespace Solitaire.MenuCore.Implementations;

public class DifficultyMenu : Menu
{
    protected override List<MenuOption> Options { get; } = [
        new EasyDifficultyMenuOption(),
        new HardDifficultyMenuOption()
    ];

    protected override void DisplayMenu()
    {
        Console.WriteLine(FiggleFonts.Standard.Render(GameStrings.difficulty_label));
    }
}