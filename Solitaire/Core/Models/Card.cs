namespace Solitaire.Core.Models;

public class Card(int value, Suit suit)
{
    public int Value { get; } = value;
    public Suit Suit { get; } = suit;
    public ConsoleColor Color => (Suit == Suit.Hearts || Suit == Suit.Diamonds) ? ConsoleColor.Red : ConsoleColor.White;
    public bool IsShown { get; set; } = false;

    public void Display()
    {
        string val = Value switch
        {
            1 => "A", 11 => "J", 12 => "Q", 13 => "K",
            _ => Value.ToString()
        };

        string symbol = Suit switch
        {
            Suit.Hearts => "♥",
            Suit.Diamonds => "♦",
            Suit.Clubs => "♣",
            Suit.Spades => "♠"
        };

        if (!IsShown)
        {
            Console.Write("XX");
        };

        Console.ForegroundColor = Color;

        var cardString = $"{val}{symbol}";
        Console.Write($"{cardString, -4}");
        
        Console.ForegroundColor = ConsoleColor.White;
    }
}