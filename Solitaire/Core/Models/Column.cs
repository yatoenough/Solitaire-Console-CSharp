using Solitaire.Core.Utils;

namespace Solitaire.Core.Models;

public class Column
{
    public Stack<Card> HiddenCards = new();
    public List<Card> VisibleCards = [];
    
    public void FlipLastHidden()
    {
        if (HiddenCards.Count <= 0) return;
        
        var card = HiddenCards.Pop();
        card.IsShown = true;
        VisibleCards.Add(card);
    }
    
    public void HideLastVisible()
    {
        if (VisibleCards.Count <= 0) return;
        
        var lastFlippedCard = VisibleCards.Pop();
        lastFlippedCard.IsShown = false;
        HiddenCards.Push(lastFlippedCard);

    }
    
    public int CardsCount => VisibleCards.Count +  HiddenCards.Count;
}