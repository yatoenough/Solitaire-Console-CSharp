namespace Solitaire.Core.Models;

public class Pointer
{
    private int _pointerPosition;
    
    public int Position => _pointerPosition;

    public void MoveRight()
    {
        if (_pointerPosition < 6)
        {
            _pointerPosition++;
        }
        else
        {
            _pointerPosition = 0;
        }
    }

    public void MoveLeft()
    {
        if (_pointerPosition > 0)
        {
            _pointerPosition--;
        }
        else
        {
            _pointerPosition = 6;
        }
    }

    public override string ToString()
    {
        return "↑";
    }
}