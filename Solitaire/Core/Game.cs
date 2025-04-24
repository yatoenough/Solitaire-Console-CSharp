using Solitaire.Core.Models;
using Solitaire.Core.Utils;

namespace Solitaire.Core;

public class Game
{
    private List<Column> _columns = [];
    private List<Stack<Card>> _foundations = [];
    private Deck _deck = new();
    private Stack<Card> _wastePile = new();
    private GameRenderer _renderer = new();
    private Pointer _pointer = new();
    private int PointerPosition => _pointer.Position;

    private Card? _pickedCard;
    private bool _pickedFromWaste;
    private Column? _activeColumn;

    public Game()
    {
        _deck.Shuffle();

        for (int i = 0; i < 7; i++)
        {
            var column = new Column();
            for (int j = 0; j <= i; j++)
            {
                var card = _deck.DrawCard();
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
            _columns.Add(column);
        }
        
        for(int i = 0; i < 4; i++)
            _foundations.Add(new Stack<Card>());
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
                DrawFromDeck();
                break;
            case ConsoleKey.RightArrow:
                MovePointer(PointerMove.Right);
                break;
            case ConsoleKey.LeftArrow:
                MovePointer(PointerMove.Left);
                break;
            case ConsoleKey.Enter:
                if (_pickedCard != null)
                {
                    var success = MoveCard(_pickedCard, _columns[PointerPosition]);
                    if (!success) break;
                    if(_activeColumn is { VisibleCards.Count: 0 }) _activeColumn.FlipLastHidden();
                    _pickedCard = null;
                }
                else
                {
                    _pickedFromWaste = false;
                    _activeColumn = _columns[PointerPosition];
                    
                    if(_activeColumn.VisibleCards.Count == 0) break;
                    
                    
                    Card topCard = _activeColumn.VisibleCards.Last();
                    _pickedCard = topCard;
                    
                    _activeColumn.VisibleCards.Remove(topCard);
                    
                }
                break;
            case ConsoleKey.Q:
                End();
                return;
            case ConsoleKey.Backspace:
                if (_pickedFromWaste && _pickedCard != null)
                {
                    _wastePile.Push(_pickedCard);
                    _pickedFromWaste = false;
                }
                else if (_pickedCard != null && _activeColumn != null)
                {
                    _activeColumn.VisibleCards.Add(_pickedCard);
                }

                _pickedFromWaste = false;
                _pickedCard = null;
                break;
            case ConsoleKey.P:
                PickCardFromWaste();
                break;
            case ConsoleKey.F:
                if (_pickedCard != null)
                {
                    var success = SaveToFoundation(_pickedCard);
                    if (!success) break;
                }
                if(_activeColumn != null && _activeColumn.VisibleCards.Count == 0)
                    _activeColumn.FlipLastHidden();
                _pickedCard = null;
                break;
        }

        DrawBoard();
        NextMove();
    }

    private bool SaveToFoundation(Card card)
    {
        var foundation = _foundations[(int)card.Suit];
        
        if (foundation.Count == 0)
        {
            if (card.Value == 1)
            {
                foundation.Push(card);
                return true;
            }
            
        }
        else if (card.Value == foundation.First().Value + 1)
        {
            foundation.Push(card);
            return true;
        }
        
        return false;
    }

    private bool MoveCard(Card card, Column destination)
    {
        if (destination.VisibleCards.Count == 0)
        {
            if (card.Value == 13)
            {
                destination.VisibleCards.Add(card);
                return true;
            }
            
            return false;
        }
        
        var lastVisibleCard = destination.VisibleCards.Last();
        
        if(card.Value+1 != lastVisibleCard.Value || card.Color == lastVisibleCard.Color)
            return false;
        
        destination.VisibleCards.Add(card);
        
        return true;
    }

    private void PickCardFromWaste()
    {
        if(_pickedCard != null) return;
        if(_wastePile.Count > 0) _pickedCard = _wastePile.Pop();
        _pickedFromWaste = true;
    }

    private void MovePointer(PointerMove value)
    {
        switch (value)
        {
            case PointerMove.Left:
                _pointer.MoveLeft();
                break;
            case PointerMove.Right:
                _pointer.MoveRight();
                break;
        }
    }

    private void DrawBoard()
    {
        Console.Clear();
        
        _renderer.DisplayColumns(_columns);
        _renderer.DisplayPointer(_pointer);
        _renderer.DisplayPickedCard(_pickedCard);
        _renderer.DisplayFoundations(_foundations);
        _renderer.DisplayPiles(_deck, _wastePile);
    }
    
    private void DrawFromDeck(int difficulty = 1)
    {
        if (_deck.Count == difficulty - 1)
        {
            var tmp = _wastePile.ToList();
            
            Card remainingCard;
            while (_deck.Count > 0)
            {
                remainingCard = _deck.DrawCard()!;
                tmp.Add(remainingCard);
            }
            
            tmp.Shuffle();
        
            _wastePile.Clear();
        
            foreach (var card in tmp)
            {
                card.IsShown = false;
                _deck.PutCard(card);
            }
        }
        
        for (int i = 0; i < difficulty && _deck.Count > 0; i++)
        {
            Card? card = _deck.DrawCard();
            if (card != null)
            {
                card.IsShown = true;
                _wastePile.Push(card);
            }
        }
    }
}
