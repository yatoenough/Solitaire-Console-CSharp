namespace Solitaire.Menu;

public abstract class Menu
{
    public int OptionsCount;
    public abstract void Display(int pickedOption);
    public abstract void Confirm(int pickedOption);
}