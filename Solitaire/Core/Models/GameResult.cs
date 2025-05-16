namespace Solitaire.Core.Models;

public class GameResult(DateTime date, int movesCount)
{
    public DateTime Date { get; } = date;

    public int MovesCount { get; } = movesCount;
}