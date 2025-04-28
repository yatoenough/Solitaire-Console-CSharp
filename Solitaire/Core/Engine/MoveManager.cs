using Solitaire.Core.Models;

namespace Solitaire.Core.Engine;

public class MoveManager(DeckManager deckManager, List<Column> columns)
{
    private List<string> moveList = [];
    private DeckManager deckManager = deckManager;
    private List<Column> columns = columns;

    public void RegisterMove(string move)
    {
        moveList.Add(move);
        
        if (moveList.Count > 3)
        {
            moveList.RemoveAt(0);
        }
    }
    
    public void DiscardLastMove()
    {
        if (moveList.Count <= 0) return;

        var lastMove = moveList.Last();
        ParseMove(lastMove);
        moveList.Remove(lastMove);
    }

    private void ParseMove(string move)
    {
        foreach (var character in move)
        {
            switch (character)
            {
                case 'd':
                    var drawnCard = deckManager.PickFromWaste();
                    if(drawnCard != null) deckManager.ReturnToDeck(drawnCard);
                    break;
            }
        }
    }
}