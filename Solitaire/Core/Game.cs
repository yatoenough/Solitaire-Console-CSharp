using Solitaire.Core.Models;

namespace Solitaire.Core;

public class Game
{
    public void Start()
    {
        var card = new Card(1, Suit.Spades);
        
        Console.WriteLine(card);
    }
}
