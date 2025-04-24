namespace Solitaire.Core.Models;

public class Column
{
    public Stack<Card> HiddenCards = new Stack<Card>();
    public List<Card> VisibleCards = new List<Card>();
    
    public void FlipLastHidden()
    {
        if (HiddenCards.Count > 0)
        {
            var card = HiddenCards.Pop();
            card.IsShown = true;
            VisibleCards.Add(card);
        }
    }
    
    public int CardsCount => VisibleCards.Count +  HiddenCards.Count;
}