namespace Solitaire.Core.MenuCore.Options;

public class EmptyMenuOption(string label) : MenuOption
{
    public override string GetLabel()
    {
        return label;
    }

    public override void Execute() { }
}