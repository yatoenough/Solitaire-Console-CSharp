namespace Solitaire.MenuCore.Options;

public abstract class MenuOption
{
    public abstract string GetLabel();
    public abstract void Execute();
}