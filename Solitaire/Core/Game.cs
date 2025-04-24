using Solitaire.Core.Engine;
using Solitaire.Core.Models;
using Solitaire.Core.Rendering;
using Solitaire.Core.Utils;

namespace Solitaire.Core;

public class Game
{
    private readonly List<Column> columns = [];
    private readonly List<Stack<Card>> foundations = [];
    private readonly DeckManager deckManager = new();
    private readonly GameRenderer renderer = new();
    private readonly Pointer pointer = new();
    private readonly MoveValidator validator = new();
    
    private List<Card>? pickedCards;
    private Column? sourceColumn;
    private bool pickedFromWaste;
    
    public Game()
    {
        InitGame();
    }
    
    public void Start()
    {
        while (true)
        {
            Console.Clear();
            renderer.Render(columns, foundations, deckManager, pointer, pickedCards);

            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.Q) break;

            HandleInput(key);
        }

        Console.Clear();
        Console.WriteLine("Bye!");
    }

    private void InitGame()
    {
        for (int i = 0; i < 7; i++)
        {
            var column = new Column();
            for (int j = 0; j <= i; j++)
            {
                var card = deckManager.DrawFromDeck();
                
                if(j == i)
                    card.IsShown = true;
                else
                    column.HiddenCards.Push(card);
                
                if(card.IsShown)
                    column.VisibleCards.Add(card);
            }
            columns.Add(column);
        }
        
        for(int i = 0; i < 4; i++)
            foundations.Add(new Stack<Card>());
    }

    private void HandleInput(ConsoleKey key)
    { 
        switch(key)
        {
            case ConsoleKey.RightArrow:
                pointer.MoveRight();
                break;
            case ConsoleKey.LeftArrow:
                pointer.MoveLeft();
                break;
                
            case ConsoleKey.Enter:
                HandleEnterKey();
                break;
            case ConsoleKey.Backspace:
                PutCardBack();
                break;
                
            case ConsoleKey.D:
                deckManager.DrawCardToWaste();
                break;
            case ConsoleKey.P:
                PickCardFromWaste();
                break;
            case ConsoleKey.F:
                StoreCardInFoundation();
                break;
        }
    }

    private void HandleEnterKey()
    {
        if (pickedCards != null)
        {
            var target = columns[pointer.Position];
            if (validator.CanPlaceOnColumn(pickedCards.First(), target))
            {
                target.VisibleCards.AddRange(pickedCards);
                if (sourceColumn?.VisibleCards.Count == 0)
                    sourceColumn.FlipLastHidden();

                pickedCards = null;
                sourceColumn = null;
            }
        }
        else
        {
            sourceColumn = columns[pointer.Position];
            if (sourceColumn.VisibleCards.Count > 0)
            {
                int index = SelectPickableIndex(sourceColumn);
                pickedCards = sourceColumn.VisibleCards.GetRange(index, sourceColumn.VisibleCards.Count - index);
                sourceColumn.VisibleCards.RemoveRange(index, pickedCards.Count);
                pickedFromWaste = false;
            }
        }
    }

    private int SelectPickableIndex(Column column)
    {
        return column.VisibleCards.Count - 1;
    }
    
    private void PickCardFromWaste()
    {
        if (pickedCards != null) return;

        var card = deckManager.PickFromWaste();
        if (card == null) return;

        pickedCards = new List<Card> { card };
        pickedFromWaste = true;
    }

    private void PutCardBack()
    {
        if (pickedCards == null) return;

        if (pickedFromWaste)
        {
            foreach (var card in pickedCards)
                deckManager.ReturnToWaste(card);
        }
        else
            sourceColumn?.VisibleCards.AddRange(pickedCards);

        pickedCards = null;
        sourceColumn = null;
        pickedFromWaste = false;
    }

    private void StoreCardInFoundation()
    {
        if (pickedCards == null || pickedCards.Count != 1) return;

        var card = pickedCards.First();
        var foundation = foundations[(int)card.Suit];

        if (validator.CanMoveToFoundation(card, foundation))
        {
            foundation.Push(card);

            if (sourceColumn?.VisibleCards.Count == 0)
                sourceColumn.FlipLastHidden();

            pickedCards = null;
            sourceColumn = null;
        }
    }
    
}
