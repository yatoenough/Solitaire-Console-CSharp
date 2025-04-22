using Solitaire.Core.Models;

namespace Solitaire.Core;

public class GameRenderer
{
    public void DisplayColumns(List<Column> columns)
    {
        int maxHeight = columns.Max(c => c.HiddenCards.Count + c.VisibleCards.Count);

        for (int row = 0; row < maxHeight; row++)
        {
            foreach (var column in columns)
            {
                int hiddenCount = column.HiddenCards.Count;
                int visibleCount = column.VisibleCards.Count;
                int totalCount = hiddenCount + visibleCount;

                if (row < totalCount)
                {
                    if (row < hiddenCount)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("XX  ");
                    }
                    else
                    {
                        var visibleIndex = row - hiddenCount;
                        var card = column.VisibleCards[visibleIndex];
                        Console.Write($"{card}  ");
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

    public void DisplayFoundations(List<Stack<Card>> foundations)
    {
        Console.ForegroundColor = ConsoleColor.White;
        foreach (var foundation in foundations)
        {
            if (foundation.Count > 0)
            {
                Card topCard = foundation.Peek();
                topCard.IsShown = true;
                Console.Write($"{topCard}  ");
            }
            else
            {
                Console.Write("    ");
            }
        }
        
        Console.WriteLine();
    }

    public void DisplayPiles(Stack<Card> stockPile, Stack<Card> wastePile)
    {
        Console.ForegroundColor = ConsoleColor.White;

        if(stockPile.Count > 0)
        {
            Console.Write($"Kart w stosie rezerwowym: {stockPile.Count}  ");
        }
        
        Console.WriteLine("\n");
        Console.Write("Dobrane karty: ");
        
        foreach(var card in wastePile)
        {
            Console.Write($"{card}  ");
        }
        
        Console.WriteLine();
    }
}
