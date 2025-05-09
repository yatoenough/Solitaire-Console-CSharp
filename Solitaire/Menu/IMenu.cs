using Solitaire.Menu.Options;

namespace Solitaire.Menu;

public interface IMenu
{
    List<IMenuOption> Options { get; }
    
    public void Display(int pickedOption);
    public void Confirm(int pickedOption)
    {
        Options[pickedOption].Execute();
    }
    
}