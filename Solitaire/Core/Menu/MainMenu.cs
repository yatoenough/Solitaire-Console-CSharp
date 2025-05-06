namespace Solitaire.Core.Menu;

public class MainMenu
{
    private readonly List<IMenuOption> options = [
        new StartGameMenuOption(),
        new QuitMenuOption()
    ];
    
    public int OptionsCount => options.Count;
    
    public void Display(int pickedOption)
    {
        Console.WriteLine("Solitaire");

        for (int i = 0; i < options.Count; i++)
        {
            if(pickedOption == i) Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(options[i].GetLabel());
            Console.ResetColor();
        }
        
    }

    public void Confirm(int pickedOption)
    {
        options[pickedOption].Execute();
    }
}