namespace Solitaire.Core.Menu.Options;

public class QuitMenuOption : IMenuOption
{
    public string GetLabel()
    {
        return "Quit";
    }

    public void Execute()
    {
        Console.Clear();
        Console.WriteLine("Bye!");
        Environment.Exit(0); 
    }
}