using Figgle;
using Solitaire.I18n;
using Solitaire.Menu.Options;
using Solitaire.Menu.Options.MainMenuOptions;

namespace Solitaire.Menu.Implementations;

public class EndgameMenu(int moveCount) : IMenu
{
    public List<IMenuOption> Options { get; } = [
        new StartGameMenuOption(GameStrings.restart_label),
        new BackToMainMenuOption()
    ];
    
    public void Display(int pickedOption)
    {
        Console.WriteLine(FiggleFonts.Standard.Render(GameStrings.win_label));
        Console.WriteLine($"{GameStrings.move_count_label} {moveCount}\n");
        
        for (int i = 0; i < Options.Count; i++)
        {
            if(pickedOption == i) Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"{i + 1}. {Options[i].GetLabel()}");
            Console.ResetColor();
        }
    }
}