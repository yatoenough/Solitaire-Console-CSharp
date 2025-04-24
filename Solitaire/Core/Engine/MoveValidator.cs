using Solitaire.Core.Models;

namespace Solitaire.Core.Engine;

public class MoveValidator
{
    public bool CanPlaceOnColumn(Card card, Column target)
    {
        if (target.VisibleCards.Count == 0)
            return card.Value == 13;

        var last = target.VisibleCards.Last();
        return card.Color != last.Color && card.Value == last.Value - 1;
    }

    public bool CanMoveToFoundation(Card card, Stack<Card> foundation)
    {
        if (foundation.Count == 0) return card.Value == 1;
        return foundation.Peek().Value + 1 == card.Value;
    }
}
