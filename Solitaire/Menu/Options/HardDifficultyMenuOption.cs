using Solitaire.Config;
using Solitaire.I18n;

namespace Solitaire.Menu.Options;

public class HardDifficultyMenuOption : IMenuOption
{
    public string GetLabel()
    {
        return GameStrings.difficulty_hard;
    }

    public void Execute()
    {
        Settings.Difficulty = 3;
    }
}