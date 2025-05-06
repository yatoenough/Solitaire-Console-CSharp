using Solitaire.Config;
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
                for (int i = 0; i < Settings.Difficulty; i++)
                {
                    var card = deckManager.PickFromWaste();
                    if (card != null) deckManager.ReturnToDeck(card);
                }
                break;

            case MoveType.FromWasteToColumn:
            {
                var column = columns[move.DestinationIndex];
                column.VisibleCards.Remove(move.Cards[0]);
                deckManager.ReturnToWaste(move.Cards[0]);
                break;
            }

            case MoveType.FromColumnToColumn:
            {
                var sourceColumn = columns[move.SourceIndex];
                var destinationColumn = columns[move.DestinationIndex];
                int count = move.Cards.Count;

                destinationColumn.VisibleCards.RemoveRange(destinationColumn.VisibleCards.Count - count, count); 
                
                sourceColumn.HideLastVisible();
                
                sourceColumn.VisibleCards.AddRange(move.Cards);
                break;
            }

            case MoveType.FromColumnToFoundation:
            {
                var cardInFoundation = foundations[(int)move.Cards[0].Suit].Pop();
                var column = columns[move.SourceIndex];
                
                column.HideLastVisible();
                
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
