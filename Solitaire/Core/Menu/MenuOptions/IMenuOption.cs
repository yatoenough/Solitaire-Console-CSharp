namespace Solitaire.Core.Menu;

public interface IMenuOption
{
    public string GetLabel();
    public void Execute();
}