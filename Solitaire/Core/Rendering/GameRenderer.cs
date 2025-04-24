using Solitaire.Core.Engine;
using Solitaire.Core.Models;

namespace Solitaire.Core.Rendering;

public class GameRenderer
{
    public void Render(List<Column> columns, List<Stack<Card>> foundations, DeckManager deckManager, Pointer pointer, Card? activeCard)
    {
        DisplayColumns(columns);
        DisplayPointer(pointer);
        DisplayPickedCard(activeCard);
        DisplayFoundations(foundations);
        DisplayPiles(deckManager.GetDeck(), deckManager.GetWaste());
    }
    private void DisplayColumns(List<Column> columns)
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
                        Console.Write("XX  ");
                    }
                    else
                    {
                        var visibleIndex = row - hiddenCount;
                        var card = column.VisibleCards[visibleIndex];
                        card.Display();
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

    private void DisplayFoundations(List<Stack<Card>> foundations)
    {
        Console.Write("Stosy końcowe: ");
        foreach (var foundation in foundations)
        {
            if (foundation.Count > 0)
            {
                Card topCard = foundation.Peek();
                topCard.IsShown = true;
                topCard.Display();
            }
            else
            {
                Console.Write("XX  ");
            }
        }
        
        Console.WriteLine();
    }

    private void DisplayPiles(Deck deck, Stack<Card> wastePile)
    {
        Console.Write($"Kart w stosie rezerwowym: {deck.Count}  ");
        
        Console.WriteLine("\n");
        Console.Write("Dobrane karty: ");
        
        foreach(var card in wastePile)
        {
            card.Display();
        }
        
        Console.WriteLine();
    }

    private void DisplayPointer(Pointer pointer)
    {
        for (int column = 0; column < pointer.Position; column++)
        {
            Console.Write("    ");
        }
        
        Console.WriteLine(pointer);
    }

    private void DisplayPickedCard(Card? card)
    {
        if (card != null)
        {
            Console.Write("Wybrana karta: ");
            
            card.Display();
            
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("Wybrana karta: ");
        }
        
    }
}
