using Solitaire.Config;
using Solitaire.I18n;

namespace Solitaire.Menu.Options.DifficultyOptions;

public class EasyDifficultyMenuOption : IMenuOption
{
    public string GetLabel()
    {
        return GameStrings.difficulty_easy;
    }

    public void Execute()
    {
        Settings.Difficulty = 1;
    }
}