namespace Solitaire.Core.Menu.Options;

public interface IMenuOption
{
    public string GetLabel();
    public void Execute();
}