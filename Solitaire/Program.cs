using Solitaire.Core.Models;

namespace Solitaire;

internal static class Program
{
    internal static void Main(string[] args)
    {
        var card = new Card(1, Suit.Spades);
        card.IsShown = true;
        
        Console.WriteLine(card);
    }
}