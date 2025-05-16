using Figgle;
using Solitaire.Core.Engine;
using Solitaire.Core.MenuCore.Options;
using Solitaire.I18n;

namespace Solitaire.Core.MenuCore.Implementations;

public class ScoreboardMenu : Menu
{
    protected override List<MenuOption> Options { get; } =
    [
        new EmptyMenuOption("Back")
    ];
    protected override void DisplayMenu()
    {
        Console.WriteLine(FiggleFonts.Standard.Render(GameStrings.scoreboard_label));
        
        foreach (var result in ScoreboardStore.Scoreboard)
        {
            Console.Write(result.Date);
            Console.Write($" | {GameStrings.move_count_label} {result.MovesCount}\n");
            Console.WriteLine();
        }
    }
}