using Solitaire.Core.Models;

namespace Solitaire.Core;

public class Game
{
    private Deck deck;
    private List<Column> columns = new List<Column>();
    private List<Stack<Card>> foundations = new List<Stack<Card>>();
    private Stack<Card> stockPile = new Stack<Card>();
    private Stack<Card> wastePile = new Stack<Card>();

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
        foreach (var column in columns)
        {
            foreach (var card in column.VisibleCards)
            {
                Console.WriteLine(card);
            }
        }
    }
    
    public void DrawFromStock(int difficulty = 1)
    {
        for (int i = 0; i < difficulty && stockPile.Count > 0; i++)
        {
            Card card = stockPile.Pop();
            card.IsShown = true;
            wastePile.Push(card);
        }

        if (stockPile.Count != 0) return;
        
        var tmp = wastePile.Reverse().ToList();
        wastePile.Clear();
        
        foreach (var card in tmp)
        {
            card.IsShown = false;
            stockPile.Push(card);
        }
        
    }
}
