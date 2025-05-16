using Figgle;
using Solitaire.Core.MenuCore.Options;
using Solitaire.Core.MenuCore.Options.DifficultyOptions;
using Solitaire.I18n;

namespace Solitaire.Core.MenuCore.Implementations;

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