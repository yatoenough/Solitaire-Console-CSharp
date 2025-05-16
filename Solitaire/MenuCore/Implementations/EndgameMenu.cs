using Figgle;
using Solitaire.I18n;
using Solitaire.MenuCore.Options;
using Solitaire.MenuCore.Options.MainMenuOptions;

namespace Solitaire.MenuCore.Implementations;

public class EndgameMenu(int moveCount) : Menu
{
    public override List<MenuOption> Options { get; } = [
        new StartGameMenuOption(GameStrings.restart_label),
        new BackToMainMenuOption()
    ];
    
    public override void Display()
    {
        Console.WriteLine(FiggleFonts.Standard.Render(GameStrings.win_label));
        Console.WriteLine($"{GameStrings.move_count_label} {moveCount}\n");
        
        DisplayOptions();
    }
}