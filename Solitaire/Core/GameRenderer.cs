using Solitaire.Core.Models;

namespace Solitaire.Core;

public class GameRenderer
{
    public void DisplayColumns(List<Column> columns)
    {
        int maxHeight = columns.Max(c => c.HiddenCards.Count + c.VisibleCards.Count);

        for (int row = 0; row < maxHeight; row++)
        {
            for (int col = 0; col < columns.Count; col++)
            {
                var column = columns[col];
                int hiddenCount = column.HiddenCards.Count;
                int visibleCount = column.VisibleCards.Count;
                int totalCount = hiddenCount + visibleCount;

                if (row < totalCount)
                {
                    if (row < hiddenCount)
                    {
                        Console.Write("XX  ");
                    }
                    else
                    {
                        var visibleIndex = row - hiddenCount;
                        var card = column.VisibleCards[visibleIndex];
                        Console.Write($"{card,-4}");
                    }
                }
                else
                {
                    Console.Write("    ");
                }
            }
            Console.WriteLine();
        }
    }
}