using Solitaire.Core.Models;
using Solitaire.Core.Utils;

namespace Solitaire.Core.Engine;

public class DeckManager
{
    private readonly Deck deck = new();
    private readonly Stack<Card> waste = new();

    public DeckManager()
    {
        deck.Shuffle();
    }

    public Card DrawFromDeck() => deck.DrawCard()!;
    public void DrawCardToWaste(int difficulty = 1)
    {
        if (deck.Count < difficulty)
            RecycleWasteToDeck();

        for (int i = 0; i < difficulty && deck.Count > 0; i++)
        {
            var card = deck.DrawCard();
            if (card == null) break;

            card.IsShown = true;
            waste.Push(card);
        }
    }

    public Card? PickFromWaste() => waste.Count > 0 ? waste.Pop() : null;
    public void ReturnToWaste(Card card) => waste.Push(card);
    public Stack<Card> GetWaste() => waste;
    public Deck GetDeck() => deck;
    
    private void RecycleWasteToDeck()
    {
        var cardsToRecycle = waste.ToList();

        while (deck.Count > 0)
        {
            var card = deck.DrawCard();
            if (card != null)
                cardsToRecycle.Add(card);
        }

        cardsToRecycle.Shuffle();

        waste.Clear();
        foreach (var card in cardsToRecycle)
        {
            card.IsShown = false;
            deck.PutCard(card);
        }
    }
}
