namespace Solitaire.Core.Models;

public class Column
{
    public Stack<Card> HiddenCards = new Stack<Card>();
    public List<Card> VisibleCards = new List<Card>();
    
    public void AddCard(Card card)
    {
        card.IsShown = true;
        VisibleCards.Add(card);
    }

    public void FlipLastHidden()
    {
        if (HiddenCards.Count > 0)
        {
            var card = HiddenCards.Pop();
            card.IsShown = true;
            VisibleCards.Add(card);
        }
    }
}