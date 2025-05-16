using Solitaire.MenuCore.Options;
using Solitaire.MenuCore.Options.MainMenuOptions;

namespace Solitaire.MenuCore.Implementations;

public class QuitConfirmationMenu : Menu
{
    public override List<MenuOption> Options { get; } =
    [
        new QuitMenuOption(),
        new EmptyMenuOption("Don't quit")
    ];
    
    public override void Display(int pickedOption)
    {
        Console.WriteLine("Are you sure you want to quit?\n");
        
        for (int i = 0; i < Options.Count; i++)
        {
            if(pickedOption == i) Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"{i + 1}. {Options[i].GetLabel()}");
            Console.ResetColor();
        }
    }
}