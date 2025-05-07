using Solitaire.I18n;

namespace Solitaire.Menu.Options;

public class QuitMenuOption : IMenuOption
{
    public string GetLabel()
    {
        return GameStrings.option_quit;
    }

    public void Execute()
    {
        Console.Clear();
        Environment.Exit(0); 
    }
}