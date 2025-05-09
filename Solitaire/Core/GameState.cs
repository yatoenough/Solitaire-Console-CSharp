using Solitaire.Core.Engine;
using Solitaire.Core.Models;

namespace Solitaire.Core;

public class GameState
{
    public readonly List<Column> Columns = [];
    public readonly List<Stack<Card>> Foundations = [];
    public readonly DeckManager DeckManager = new();
    public readonly Pointer Pointer = new();
    public List<Card>? PickedCards;
    
    public Column? SourceColumn;
    public bool PickedFromWaste;
    
    public bool IsSelectingRange;
    public int RangeStartIndex = -1;
}
