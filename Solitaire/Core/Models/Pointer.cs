namespace Solitaire.Core.Models;

public class Pointer
{
    public int Position { get; private set; }
    
    public void MoveRight()
    {
        if (Position < 6)
        {
            Position++;
        }
        else
        {
            Position = 0;
        }
    }

    public void MoveLeft()
    {
        if (Position > 0)
        {
            Position--;
        }
        else
        {
            Position = 6;
        }
    }

    public override string ToString()
    {
        return "↑";
    }
}