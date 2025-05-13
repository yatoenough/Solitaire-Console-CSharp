using Solitaire.Config;
using Solitaire.I18n;

namespace Solitaire.MenuCore.Options.DifficultyOptions;

public class EasyDifficultyMenuOption : MenuOption
{
    public override string GetLabel()
    {
        return GameStrings.difficulty_easy;
    }

    public override void Execute()
    {
        Settings.Difficulty = 1;
    }
}