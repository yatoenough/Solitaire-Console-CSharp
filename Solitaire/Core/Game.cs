using Solitaire.Core.Engine;
using Solitaire.Core.Models;
using Solitaire.Core.Rendering;
using Solitaire.Core.Utils;

namespace Solitaire.Core;

public class Game
{
    private readonly List<Column> columns = [];
    private readonly List<Stack<Card>> foundations = [];
    private readonly Deck deck = new();
    private readonly Stack<Card> wastePile = new();
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
            GameRenderer.Render(columns, foundations, deck, wastePile, pointer, activeCard);

            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.Q) break;

            HandleInput(key);
        }

        Console.Clear();
        Console.WriteLine("Bye!");
    }

    private void InitGame()
    {
        deck.Shuffle();

        for (int i = 0; i < 7; i++)
        {
            var column = new Column();
            for (int j = 0; j <= i; j++)
            {
                var card = deck.DrawCard();
                if (card == null) break;
                
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
                MovePointer(PointerMove.Right);
                break;
            case ConsoleKey.LeftArrow:
                MovePointer(PointerMove.Left);
                break;
                
            case ConsoleKey.Enter:
                PickOrMoveCard();
                break;
            case ConsoleKey.Backspace:
                PutCardBack();
                break;
                
            case ConsoleKey.D:
                DrawFromDeck();
                break;
            case ConsoleKey.P:
                PickCardFromWaste();
                break;
            case ConsoleKey.F:
                StoreCardInFoundation();
                break;
        }
    }

    private void PickOrMoveCard()
    {
        if (activeCard != null)
        {
            var success = MoveCardToColumn(activeCard, columns[pointer.Position]);
            if (!success) return;
            if(activeColumn is { VisibleCards.Count: 0 }) activeColumn.FlipLastHidden();
            activeCard = null;
        }
        else
        {
            pickedFromWaste = false;
            activeColumn = columns[pointer.Position];
                    
            if(activeColumn.VisibleCards.Count == 0) return;
        
            activeCard = activeColumn.VisibleCards.Pop();
        }
    }

    private void PutCardBack()
    {
        if(activeCard == null) return;
        
        if (pickedFromWaste)
        {
            wastePile.Push(activeCard);
        }
        else
        {
            activeColumn?.VisibleCards.Add(activeCard);
        }

        pickedFromWaste = false;
        activeCard = null;
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

    private bool MoveCardToColumn(Card card, Column destination)
    {
        if (destination.VisibleCards.Count == 0)
        {
            if (card.Value != 13) return false;
            destination.VisibleCards.Add(card);
            return true;
        }
        
        var lastVisibleCard = destination.VisibleCards.Last();

        var cardCanBePlaced = card.Value == lastVisibleCard.Value - 1 && card.Color != lastVisibleCard.Color;
        
        if(!cardCanBePlaced)
            return false;
        
        destination.VisibleCards.Add(card);
        
        return true;
    }

    private void PickCardFromWaste()
    {
        if(activeCard != null) return;
        if(wastePile.Count > 0) activeCard = wastePile.Pop();
        pickedFromWaste = true;
    }

    private void MovePointer(PointerMove value)
    {
        switch (value)
        {
            case PointerMove.Left:
                pointer.MoveLeft();
                break;
            case PointerMove.Right:
                pointer.MoveRight();
                break;
        }
    }
    
    private void DrawFromDeck(int difficulty = 1)
    {
        if (deck.Count == difficulty - 1)
        {
            var tmp = wastePile.ToList();

            while (deck.Count > 0)
            {
                var remainingCard = deck.DrawCard()!;
                tmp.Add(remainingCard);
            }
            
            tmp.Shuffle();
        
            wastePile.Clear();
        
            foreach (var card in tmp)
            {
                card.IsShown = false;
                deck.PutCard(card);
            }
        }
        
        for (int i = 0; i < difficulty && deck.Count > 0; i++)
        {
            var card = deck.DrawCard();
            if (card == null) break;
            card.IsShown = true;
            wastePile.Push(card);
        }
    }
}
