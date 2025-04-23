using Solitaire.Core.Models;
using Solitaire.Core.Utils;

namespace Solitaire.Core;

public class Game
{
    private Deck deck;
    private List<Column> columns = new List<Column>();
    private List<Stack<Card>> foundations = new List<Stack<Card>>();
    private Stack<Card> stockPile = new Stack<Card>();
    private Stack<Card> wastePile = new Stack<Card>();
    private GameRenderer renderer = new GameRenderer();
    
    private int pointerPosition = 0;
    private Column pickedColumn = null;

    public Game()
    {
        deck = new Deck();
        deck.Shuffle();

        for (int i = 0; i < 7; i++)
        {
            var column = new Column();
            for (int j = 0; j <= i; j++)
            {
                var card = deck.DrawCard();
                if (card != null)
                {
                    if(j == i)
                        card.IsShown = true;
                    else
                        column.HiddenCards.Push(card);
                
                    if(card.IsShown)
                        column.VisibleCards.Add(card);
                }
            }
            columns.Add(column);
        }
        
        Card? cardFromDeck = deck.DrawCard();
        while (cardFromDeck != null)
        {
            stockPile.Push(cardFromDeck);
            cardFromDeck = deck.DrawCard();
        }
        
        for(int i = 0; i < 4; i++)
            foundations.Add(new Stack<Card>());
        
    }
    
    public void Start()
    {
        DrawBoard();
        NextMove();
    }

    public void End()
    {
        Console.Clear();
        Console.WriteLine("Bye!");
    }

    private void NextMove()
    {
        var pressedKey = UserInputProvider.Get();
        switch(pressedKey)
        {
            case ConsoleKey.D:
                DrawFromStock();
                break;
            case ConsoleKey.RightArrow:
                MovePointer(PointerMove.Right);
                break;
            case ConsoleKey.LeftArrow:
                MovePointer(PointerMove.Left);
                break;
            case ConsoleKey.Enter:
                if (pickedColumn != null)
                {
                    var card = pickedColumn.VisibleCards.Last();
                    pickedColumn.VisibleCards.Remove(card);
                    
                    MoveCard(card, PickColumn());
                    if(pickedColumn.VisibleCards.Count == 0) pickedColumn.FlipLastHidden();
                    pickedColumn = null;
                }
                else
                {
                    pickedColumn = PickColumn();
                }
                break;
            case ConsoleKey.Q:
                End();
                return;
        }

        DrawBoard();
        NextMove();
    }

    private void MoveCard(Card card, Column destination)
    {
        destination.VisibleCards.Add(card);
    }

    private Column PickColumn()
    {
        return columns[pointerPosition];
    }

    private void MovePointer(PointerMove value)
    {
        switch (value)
        {
            case PointerMove.Left:
                if (pointerPosition > 0)
                {
                    pointerPosition--;
                }
                else
                {
                    pointerPosition = 6;
                }
                break;
            case PointerMove.Right:
                if (pointerPosition < 6)
                {
                    pointerPosition++;
                }
                else
                {
                    pointerPosition = 0;
                }
                break;
        }
    }

    private void DrawBoard()
    {
        Console.Clear();
        
        renderer.DisplayColumns(columns);
        renderer.DisplayPointer(pointerPosition);
        renderer.DisplayPickedCard(pickedColumn);
        renderer.DisplayFoundations(foundations);
        renderer.DisplayPiles(stockPile, wastePile);
    }
    
    private void DrawFromStock(int difficulty = 1)
    {
        for (int i = 0; i < difficulty && stockPile.Count >= 0; i++)
        {
            if(stockPile.Count == 0) break;
            
            Card card = stockPile.Pop();
            card.IsShown = true;
            wastePile.Push(card);
            
            return;
        }
        
        var tmp = wastePile.ToList();
        tmp.Shuffle();
        
        wastePile.Clear();
        
        foreach (var card in tmp)
        {
            card.IsShown = false;
            stockPile.Push(card);
        }
        
        DrawFromStock();
    }
}
