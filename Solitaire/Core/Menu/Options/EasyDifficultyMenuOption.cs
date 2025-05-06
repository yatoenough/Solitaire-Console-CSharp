namespace Solitaire.Core.Menu.Options;

public class EasyDifficultyMenuOption : IMenuOption
{
    public string GetLabel()
    {
        return "Easy";
    }

    public void Execute()
    {
        Settings.Difficulty = 1;
    }
}