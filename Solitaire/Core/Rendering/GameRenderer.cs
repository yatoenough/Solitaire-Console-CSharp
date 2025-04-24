using Solitaire.Core.Engine;
using Solitaire.Core.Models;

namespace Solitaire.Core.Rendering;

public class GameRenderer
{
    public void Render(List<Column> columns, List<Stack<Card>> foundations, DeckManager deckManager, Pointer pointer, List<Card>? pickedCards, int rangeStartIndex, bool isSelectingRange)
    {
        DisplayColumns(columns, pointer, rangeStartIndex, isSelectingRange);
        DisplayPointer(pointer);
        DisplayPickedCards(pickedCards);
        DisplayFoundations(foundations);
        DisplayPiles(deckManager.GetDeck(), deckManager.GetWaste());
    }
    
    private void DisplayColumns(List<Column> columns, Pointer pointer, int rangeStartIndex, bool isSelectingRange)
    {
        int maxHeight = columns.Max(c => c.HiddenCards.Count + c.VisibleCards.Count);

        for (int row = 0; row < maxHeight; row++)
        {
            for (int colIndex = 0; colIndex < columns.Count; colIndex++)
            {
                var column = columns[colIndex];
                int hidden = column.HiddenCards.Count;
                int visible = column.VisibleCards.Count;
                int total = hidden + visible;

                if (row < total)
                {
                    if (row < hidden)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("XX  ");
                    }
                    else
                    {
                        var cardIndex = row - hidden;
                        var card = column.VisibleCards[cardIndex];

                        if (isSelectingRange && colIndex == pointer.Position && cardIndex >= rangeStartIndex)
                            card.IsSelected = true;
                        else
                            card.IsSelected = false;

                        card.Display();
                    }
                }
                else
                {
                    Console.Write("    ");
                }
            }
            Console.ResetColor();
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

    private void DisplayPickedCards(List<Card>? cards)
    {
        Console.Write("Wybrane karty: ");
        if (cards != null)
        {
            foreach (var card in cards)
                card.Display();
        }
        Console.WriteLine();
        
    }
}
