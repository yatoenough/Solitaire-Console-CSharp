using Solitaire.I18n;

namespace Solitaire.MenuCore.Options.MainMenuOptions;

public class QuitMenuOption : MenuOption
{
    public override string GetLabel()
    {
        return GameStrings.option_quit;
    }

    public override void Execute()
    {
        Console.Clear();
        Environment.Exit(0); 
    }
}