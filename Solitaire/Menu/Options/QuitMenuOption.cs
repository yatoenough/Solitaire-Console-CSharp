namespace Solitaire.Menu.Options;

public class QuitMenuOption : IMenuOption
{
    public string GetLabel()
    {
        return "Quit";
    }

    public void Execute()
    {
        Console.Clear();
        Environment.Exit(0); 
    }
}