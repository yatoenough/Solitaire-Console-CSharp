namespace Solitaire.Menu;

public class MenuOptionPicker(int limit)
{
    public int PickedOption { get; private set; }

    public void OnUpArrowPressed()
    {
        if (PickedOption <= 0)
        {
            PickedOption = limit - 1;
            return;
        }
        
        PickedOption--;
    }
    
    public void OnDownArrowPressed()
    {
        if (PickedOption >= limit - 1)
        {
            PickedOption = 0;
            return;
        }
        
        PickedOption++;
    }
}