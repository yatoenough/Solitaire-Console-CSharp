using Solitaire.Config;
using Solitaire.I18n;

namespace Solitaire.Core.MenuCore.Options.DifficultyOptions;

public class HardDifficultyMenuOption : MenuOption
{
    public override string GetLabel()
    {
        return GameStrings.difficulty_hard;
    }

    public override void Execute()
    {
        Settings.Difficulty = 3;
    }
}