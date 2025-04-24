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
    
    private Card? activeCard;
    private Column? activeColumn;
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
            renderer.Render(columns, foundations, deckManager, pointer, activeCard);

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
        if (activeCard != null)
        {
            var target = columns[pointer.Position];
            if (validator.CanPlaceOnColumn(activeCard, target))
            {
                target.VisibleCards.Add(activeCard);
                if (activeColumn?.VisibleCards.Count == 0)
                    activeColumn?.FlipLastHidden();

                activeCard = null;
            }
        }
        else
        {
            activeColumn = columns[pointer.Position];
            if (activeColumn.VisibleCards.Count > 0)
            {
                activeCard = activeColumn.VisibleCards.Pop();
                pickedFromWaste = false;
            }
        }
    }
    
    private void PickCardFromWaste()
    {
        if (activeCard != null) return;

        var card = deckManager.PickFromWaste();
        
        if (card == null) return;
        
        activeCard = card;
        pickedFromWaste = true;
    }

    private void PutCardBack()
    {
        if (activeCard == null) return;

        if (pickedFromWaste)
            deckManager.ReturnToWaste(activeCard);
        else
            activeColumn?.VisibleCards.Add(activeCard);

        activeCard = null;
        pickedFromWaste = false;
    }

    private void StoreCardInFoundation()
    {
        if(activeCard == null) return;
        
        var foundation = foundations[(int)activeCard.Suit];
        if (validator.CanMoveToFoundation(activeCard, foundation))
        {
            foundation.Push(activeCard);
            if (activeColumn?.VisibleCards.Count == 0)
                activeColumn?.FlipLastHidden();
            activeCard = null;
        }
    }
    
}
