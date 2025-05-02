namespace Solitaire.Core.Models;

public class Move
{
    public MoveType Type { get; set; }
    public List<Card> Cards { get; set; }
    public int SourceIndex { get; set; } = -1;
    public int DestinationIndex { get; set; } = -1;
}