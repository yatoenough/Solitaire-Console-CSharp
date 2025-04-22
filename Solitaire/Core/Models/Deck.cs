namespace Solitaire.Core.Models;

public class Deck
{
    private List<Card> _cards = new List<Card>();

    public Deck()
    {
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            for (int i = 1; i <= 13; i++)
            {
                _cards.Add(new Card(i, suit));
            }
        }
    }
    
    public void Shuffle()
    {
        var random = new Random();
        _cards = _cards.OrderBy(c => random.Next()).ToList();
    }
    
    public Card? DrawCard()
    {
        if (_cards.Count == 0) return null;
        
        Card card = _cards[0];
        _cards.RemoveAt(0);
        
        return card;
    }
    
    public int Count => _cards.Count;

}