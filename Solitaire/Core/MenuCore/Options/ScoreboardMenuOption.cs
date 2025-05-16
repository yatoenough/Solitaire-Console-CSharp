using Solitaire.Core.MenuCore.Implementations;
using Solitaire.I18n;

namespace Solitaire.Core.MenuCore.Options;

public class ScoreboardMenuOption : MenuOption
{
    public override string GetLabel()
    {
        return GameStrings.scoreboard_label;
    }

    public override void Execute()
    {
        Menu.Handle(new ScoreboardMenu());
    }
}