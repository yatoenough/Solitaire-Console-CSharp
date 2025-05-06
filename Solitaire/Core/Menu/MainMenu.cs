using Figgle;
using Solitaire.Core.Menu.Options;

namespace Solitaire.Core.Menu;

public class MainMenu : Menu
{
    private readonly List<IMenuOption> options = [
        new StartGameMenuOption(),
        new QuitMenuOption()
    ];
    
    public new int OptionsCount => options.Count;

    public override void Display(int pickedOption)
    {
        Console.WriteLine(FiggleFonts.Slant.Render("Solitaire"));
        Console.WriteLine("Crafted by Nikita Shyshkin [https://github.com/yatoenough]\n");

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