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

    public void DisplayFoundations(List<Stack<Card>> foundations)
    {
        Console.ForegroundColor = ConsoleColor.White;
        
        Console.Write("Stosy końcowe: ");
        foreach (var foundation in foundations)
        {
            if (foundation.Count > 0)
            {
                Card topCard = foundation.Peek();
                topCard.IsShown = true;
                Console.Write($"{topCard,-4}");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.Write("XX  ");
            }
        }
        
        Console.WriteLine();
    }

    public void DisplayPiles(Deck deck, Stack<Card> wastePile)
    {
        Console.ForegroundColor = ConsoleColor.White;

        Console.Write($"Kart w stosie rezerwowym: {deck.Count}  ");
        
        Console.WriteLine("\n");
        Console.Write("Dobrane karty: ");
        
        foreach(var card in wastePile)
        {
            Console.Write($"{card,-4}");
        }
        
        Console.WriteLine();
    }

    public void DisplayPointer(Pointer pointer)
    {
        Console.ForegroundColor = ConsoleColor.White;
        for (int column = 0; column < pointer.Position; column++)
        {
            Console.Write("    ");
        }
        
        Console.WriteLine(pointer);
    }

    public void DisplayPickedCard(Card? card)
    {
        Console.ForegroundColor = ConsoleColor.White;
        if (card != null)
        {
            Console.Write("Wybrana karta: ");
            Console.WriteLine($"{card}");
        }
        else
        {
            Console.WriteLine("Wybrana karta: ");
        }
        
    }
}
