using Solitaire.Core.Utils;

namespace Solitaire.Core.Models;

public class Deck
{
    private List<Card> cards = new();

    public Deck()
    {
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            for (int i = 1; i <= 13; i++)
            {
                cards.Add(new Card(i, suit));
            }
        }
    }
    
    public int Count => cards.Count;
    
    public void Shuffle()
    {
        cards.Shuffle();
    }
    
    public Card? DrawCard()
    {
        if (cards.Count == 0) return null;
        
        Card card = cards[0];
        cards.RemoveAt(0);
        
        return card;
    }

    public void PutCard(Card card)
    {
        cards.Insert(0, card);
    }
    
}