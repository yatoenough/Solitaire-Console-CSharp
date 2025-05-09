using Solitaire.Core.Engine;
using Solitaire.Core.Models;
using Solitaire.I18n;

namespace Solitaire.Core.Rendering;

public class GameRenderer
{
    public void Render(GameState state)
    {
        var deckManager = state.DeckManager;
        var pointer = state.Pointer;
        var columns = state.Columns;
        var pickedCards = state.PickedCards;
        var foundations = state.Foundations;
        
        DisplayColumns(columns, pointer, state.RangeStartIndex, state.IsSelectingRange);
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
                int total = column.CardsCount;

                if (row < total)
                {
                    if (row < hidden)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
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
        Console.Write($@"{GameStrings.game_foundations}: ");
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
        Console.Write($"{GameStrings.game_stock_label}: {deck.Count}  ");
        
        Console.WriteLine("\n");
        Console.Write($"{GameStrings.game_waste_label}: ");
        
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
        Console.Write($"{GameStrings.game_selectedcard_label}: ");
        if (cards != null)
        {
            foreach (var card in cards)
                card.Display();
        }
        Console.WriteLine();
        
    }
}
