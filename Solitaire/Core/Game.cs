using Solitaire.Core.Engine;
using Solitaire.Core.Models;
using Solitaire.Core.Rendering;
using Solitaire.Core.Utils;
using Solitaire.MenuCore;
using Solitaire.MenuCore.Implementations;

namespace Solitaire.Core;

public class Game
{
    private GameState state = new();
    private readonly GameRenderer renderer = new();
    private readonly MoveValidator validator = new();
    private MoveManager moveManager;
    private int difficulty;

    public Game(int difficulty = 1) {
        this.difficulty = difficulty;
        InitGame();
        moveManager = new MoveManager(state.DeckManager, state.Columns, state.Foundations);
    }
    
    public void Start()
    {
        while (true)
        {
            Console.Clear();
            if (CheckIfWin())
            {
                Menu.Handle(new EndgameMenu(moveManager.MoveCount));
                return;
            }
            renderer.Render(state);

            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.Q:
                {
                    Menu.Handle(new QuitConfirmationMenu());
                    break;
                }
                case ConsoleKey.R:
                {
                    Menu.Handle(new RestartMenu());
                    break;
                }
            }

            HandleInput(key);
        }
    }

    private bool CheckIfWin()
    {
        bool win = true;
        
        foreach (var column in state.Columns)
        {
            if(column.HiddenCards.Count > 0) win = false;
        }
        
        return win; 
    }

    private void InitGame()
    {
        state = new();
        
        for (int i = 0; i < 7; i++)
        {
            var column = new Column();
            for (int j = 0; j <= i; j++)
            {
                var card = state.DeckManager.DrawFromDeck()!;
                
                if(j == i)
                    card.IsShown = true;
                else
                    column.HiddenCards.Push(card);
                
                if(card.IsShown)
                    column.VisibleCards.Add(card);
            }
            state.Columns.Add(column);
        }
        
        for(int i = 0; i < 4; i++)
            state.Foundations.Add(new Stack<Card>());
    }

    private void HandleInput(ConsoleKey key)
    {
        if (state.IsSelectingRange)
        {
            HandleRangeSelectionInput(key);
            return;
        }
        
        switch(key)
        {
            case ConsoleKey.RightArrow:
                state.Pointer.MoveRight();
                break;
            case ConsoleKey.LeftArrow:
                state.Pointer.MoveLeft();
                break;
                
            case ConsoleKey.Enter:
                HandleEnterKey();
                break;
            case ConsoleKey.Backspace:
                PutCardBack();
                break;
                
            case ConsoleKey.D:
                PickCardFromStock();
                break;
            case ConsoleKey.P:
                PickCardFromWaste();
                break;
            case ConsoleKey.F:
                StoreCardInFoundation();
                break;
            case ConsoleKey.M:
                EnterRangeSelection();
                break;
            case ConsoleKey.B:
                moveManager.UndoLastMove();
                break;
        }
    }
    
    private void HandleEnterKey()
    {
        if (state.PickedCards != null)
        {
            var target = state.Columns[state.Pointer.Position];
            if (validator.CanPlaceOnColumn(state.PickedCards.First(), target))
            {
                target.VisibleCards.AddRange(state.PickedCards);
                if (state.SourceColumn?.VisibleCards.Count == 0)
                    state.SourceColumn.FlipLastHidden();

                Move move;

                if (state.PickedFromWaste)
                {
                    move = new Move
                    {
                        Type = MoveType.FromWasteToColumn,
                        Cards = state.PickedCards,
                        DestinationIndex = state.Pointer.Position,
                    };
                }
                else
                {
                    move = new Move
                    {
                        Type = MoveType.FromColumnToColumn,
                        Cards = state.PickedCards,
                        SourceIndex = state.Columns.IndexOf(state.SourceColumn),
                        DestinationIndex = state.Pointer.Position,
                    };
                }
                
                moveManager.RegisterMove(move);

                state.PickedCards = null;
                state.SourceColumn = null;
            }
        }
        else
        {
            state.SourceColumn = state.Columns[state.Pointer.Position];
            if (state.SourceColumn.VisibleCards.Count > 0)
            {
                var card = state.SourceColumn.VisibleCards.Pop();
                state.PickedCards = [card];
                state.PickedFromWaste = false;
            }
        }
    }

    private void PickCardFromWaste()
    {
        if (state.PickedCards != null) return;

        var card = state.DeckManager.PickFromWaste();
        if (card == null) return;

        state.PickedCards = [card];
        state.PickedFromWaste = true;
    }

    private void PickCardFromStock()
    {
        if(state.PickedCards != null) return;
        
        moveManager.RegisterMove(new Move { Type = MoveType.DrawFromDeck });
        state.DeckManager.DrawCardToWaste(difficulty);
    }

    private void PutCardBack()
    {
        if (state.PickedCards == null) return;

        if (state.PickedFromWaste)
        {
            foreach (var card in state.PickedCards)
                state.DeckManager.ReturnToWaste(card);
        }
        else
            state.SourceColumn?.VisibleCards.AddRange(state.PickedCards);

        state.PickedCards = null;
        state.SourceColumn = null;
        state.PickedFromWaste = false;
    }

    private void StoreCardInFoundation()
    {
        if (state.PickedCards == null || state.PickedCards.Count != 1) return;

        var card = state.PickedCards.First();
        var foundation = state.Foundations[(int)card.Suit];

        if (validator.CanMoveToFoundation(card, foundation))
        {
            foundation.Push(card);

            if (state.SourceColumn?.VisibleCards.Count == 0)
                state.SourceColumn.FlipLastHidden();
            
            Move move;

            if (state.PickedFromWaste)
            {
                move = new Move
                {
                    Type = MoveType.FromWasteToFoundation,
                    Cards = state.PickedCards,
                };
            }
            else
            {
                move = new Move
                {
                    Type = MoveType.FromColumnToFoundation,
                    Cards = state.PickedCards,
                    SourceIndex = state.Columns.IndexOf(state.SourceColumn),
                };
            }
            
            moveManager.RegisterMove(move);

            state.PickedCards = null;
            state.SourceColumn = null;
        }
    }
    
    private void EnterRangeSelection()
    {
        var column = state.Columns[state.Pointer.Position];
        if (column.VisibleCards.Count == 0 || state.PickedCards != null) return;

        state.IsSelectingRange = true;
        state.RangeStartIndex = column.VisibleCards.Count - 1;
    }
    
    private void HandleRangeSelectionInput(ConsoleKey key)
    {
        var column = state.Columns[state.Pointer.Position];
        int maxIndex = column.VisibleCards.Count - 1;

        switch (key)
        {
            case ConsoleKey.UpArrow:
                if (state.RangeStartIndex > 0)
                    state.RangeStartIndex--;
                break;
            case ConsoleKey.DownArrow:
                if (state.RangeStartIndex < maxIndex)
                    state.RangeStartIndex++;
                break;
            case ConsoleKey.Enter:
                PickRange(column, state.RangeStartIndex);
                state.IsSelectingRange = false;
                break;
            case ConsoleKey.Backspace:
                state.IsSelectingRange = false;
                state.RangeStartIndex = -1;
                break;
        }
    }
    
    private void PickRange(Column column, int startIndex)
    {
        state.PickedCards = column.VisibleCards.GetRange(startIndex, column.VisibleCards.Count - startIndex);
        column.VisibleCards.RemoveRange(startIndex, state.PickedCards.Count);
        state.SourceColumn = column;
        state.PickedFromWaste = false;
    }

}
