using Solitaire.Config;

namespace Solitaire.Menu.Options;

public class HardDifficultyMenuOption : IMenuOption
{
    public string GetLabel()
    {
        return "Hard";
    }

    public void Execute()
    {
        Settings.Difficulty = 3;
    }
}