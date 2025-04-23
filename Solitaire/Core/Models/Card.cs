namespace Solitaire.Core.Models;

public class Card(int value, Suit suit)
{
    public int Value { get; } = value;
    public Suit Suit { get; } = suit;
    public ConsoleColor Color => (Suit == Suit.Hearts || Suit == Suit.Diamonds) ? ConsoleColor.Red : ConsoleColor.White;
    public bool IsShown { get; set; } = false;

    public override string ToString()
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
            Suit.Spades => "♠",
        };

        if (!IsShown) return "XX";

        Console.ForegroundColor = Color;

        return $"{val}{symbol}";
    }
}