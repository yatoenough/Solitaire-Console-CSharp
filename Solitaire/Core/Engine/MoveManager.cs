using Solitaire.Core.Models;
using Solitaire.Core.Utils;

namespace Solitaire.Core.Engine;

public class MoveManager(DeckManager deckManager, List<Column> columns, List<Stack<Card>> foundations)
{
    private readonly List<Move> moveHistory = [];

    public void RegisterMove(Move move)
    {
        moveHistory.Add(move);
        
        if (moveHistory.Count > 3)
        {
            moveHistory.RemoveAt(0);
        }
    }
    
    public void UndoLastMove()
    {
        if (moveHistory.Count <= 0) return;

        var lastMove = moveHistory.Pop();
        UndoMove(lastMove);
    }

    private void UndoMove(Move move)
    {
        switch (move.Type)
        {
            case MoveType.DrawFromDeck:
                var card = deckManager.PickFromWaste();
                if (card != null)
                    deckManager.ReturnToDeck(card);
                break;

            case MoveType.FromWasteToColumn:
                var col = columns[move.DestinationIndex];
                col.VisibleCards.Remove(move.Cards[0]);
                deckManager.ReturnToWaste(move.Cards[0]);
                break;

            case MoveType.FromColumnToColumn:
            {
                var sourceCol = columns[move.SourceIndex];
                var destCol = columns[move.DestinationIndex];
                int count = move.Cards.Count;

                destCol.VisibleCards.RemoveRange(destCol.VisibleCards.Count - count, count); 
                
                var lastFlippedCard = sourceCol.VisibleCards.Pop();
                lastFlippedCard.IsShown = false;
                sourceCol.HiddenCards.Push(lastFlippedCard);
                
                sourceCol.VisibleCards.AddRange(move.Cards);
                break;
            }

            case MoveType.FromColumnToFoundation:
            {
                var cardInFoundation = foundations[(int)move.Cards[0].Suit].Pop();
                var column = columns[move.SourceIndex];
                
                var lastFlippedCard = column.VisibleCards.Pop();
                lastFlippedCard.IsShown = false;
                column.HiddenCards.Push(lastFlippedCard);
                
                column.VisibleCards.Add(cardInFoundation);
                break;
            }
                
            case MoveType.FromWasteToFoundation:
            {
                var cardInFoundation = foundations[(int)move.Cards[0].Suit].Pop();
                deckManager.ReturnToWaste(cardInFoundation);
                break;
            }
                
        }
    }
}
