using Solitaire.Core.Engine;
using Solitaire.Core.Models;
using Solitaire.Core.Rendering;
using Solitaire.Core.Utils;
using Solitaire.I18n;

namespace Solitaire.Core;

public class Game
{
    private readonly List<Column> columns = [];
    private readonly List<Stack<Card>> foundations = [];
    private readonly DeckManager deckManager = new();
    private readonly GameRenderer renderer = new();
    private readonly Pointer pointer = new();
    private readonly MoveValidator validator = new();
    private MoveManager moveManager;
    private int difficulty;
    
    private List<Card>? pickedCards;
    private Column? sourceColumn;
    private bool pickedFromWaste;

    private bool isSelectingRange;
    private int rangeStartIndex = -1;
    
    public Game(int difficulty = 1) {
        this.difficulty = difficulty;
        InitGame();
        moveManager = new MoveManager(deckManager, columns, foundations);
    }
    
    public void Start()
    {
        while (true)
        {
            Console.Clear();
            if (CheckWin())
            {
                Console.WriteLine($"{GameStrings.win_label} {moveManager.MoveCount}");
                Environment.Exit(0);
            }
            renderer.Render(columns, foundations, deckManager, pointer, pickedCards, rangeStartIndex, isSelectingRange);

            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.Q) break;

            HandleInput(key);
        }
    }

    private bool CheckWin()
    {
        bool win = true;
        
        foreach (var column in columns)
            
        {
            if(column.HiddenCards.Count > 0) win = false;
        }
        
        return win; 
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
        if (isSelectingRange)
        {
            HandleRangeSelectionInput(key);
            return;
        }
        
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
                moveManager.RegisterMove(new Move { Type = MoveType.DrawFromDeck });
                deckManager.DrawCardToWaste(difficulty);
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
        if (pickedCards != null)
        {
            var target = columns[pointer.Position];
            if (validator.CanPlaceOnColumn(pickedCards.First(), target))
            {
                target.VisibleCards.AddRange(pickedCards);
                if (sourceColumn?.VisibleCards.Count == 0)
                    sourceColumn.FlipLastHidden();

                Move move;

                if (pickedFromWaste)
                {
                    move = new Move
                    {
                        Type = MoveType.FromWasteToColumn,
                        Cards = pickedCards,
                        DestinationIndex = pointer.Position,
                    };
                }
                else
                {
                    move = new Move
                    {
                        Type = MoveType.FromColumnToColumn,
                        Cards = pickedCards,
                        SourceIndex = columns.IndexOf(sourceColumn),
                        DestinationIndex = pointer.Position,
                    };
                }
                
                moveManager.RegisterMove(move);

                pickedCards = null;
                sourceColumn = null;
            }
        }
        else
        {
            sourceColumn = columns[pointer.Position];
            if (sourceColumn.VisibleCards.Count > 0)
            {
                var card = sourceColumn.VisibleCards.Pop();
                pickedCards = [card];
                pickedFromWaste = false;
            }
        }
    }

    private void PickCardFromWaste()
    {
        if (pickedCards != null) return;

        var card = deckManager.PickFromWaste();
        if (card == null) return;

        pickedCards = [card];
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
            
            Move move;

            if (pickedFromWaste)
            {
                move = new Move
                {
                    Type = MoveType.FromWasteToFoundation,
                    Cards = pickedCards,
                };
            }
            else
            {
                move = new Move
                {
                    Type = MoveType.FromColumnToFoundation,
                    Cards = pickedCards,
                    SourceIndex = columns.IndexOf(sourceColumn),
                };
            }
            
            moveManager.RegisterMove(move);

            pickedCards = null;
            sourceColumn = null;
        }
    }
    
    private void EnterRangeSelection()
    {
        var column = columns[pointer.Position];
        if (column.VisibleCards.Count == 0 || pickedCards != null) return;

        isSelectingRange = true;
        rangeStartIndex = column.VisibleCards.Count - 1;
    }
    
    private void HandleRangeSelectionInput(ConsoleKey key)
    {
        var column = columns[pointer.Position];
        int maxIndex = column.VisibleCards.Count - 1;

        switch (key)
        {
            case ConsoleKey.UpArrow:
                if (rangeStartIndex > 0)
                    rangeStartIndex--;
                break;
            case ConsoleKey.DownArrow:
                if (rangeStartIndex < maxIndex)
                    rangeStartIndex++;
                break;
            case ConsoleKey.Enter:
                PickRange(column, rangeStartIndex);
                isSelectingRange = false;
                break;
            case ConsoleKey.Backspace:
                isSelectingRange = false;
                rangeStartIndex = -1;
                break;
        }
    }
    
    private void PickRange(Column column, int startIndex)
    {
        pickedCards = column.VisibleCards.GetRange(startIndex, column.VisibleCards.Count - startIndex);
        column.VisibleCards.RemoveRange(startIndex, pickedCards.Count);
        sourceColumn = column;
        pickedFromWaste = false;
    }

}
