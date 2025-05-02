using Solitaire.Core;

namespace Solitaire;

internal static class Program
{

    private static Dictionary<int, string> menuItems = new()
    {
        {1, "Play"},
        {2, "Quit"}
    };

    private static int selectedOption = 0;
    
    internal static void Main(string[] args)
    {
        Console.Title = "Solitaire";
        DisplayMenu();
        var pickedOption = Console.ReadKey(true).KeyChar;
        
        int difficulty = 1;

        switch (pickedOption)
        {
            case '1':
                DisplayDifficulty();
                var difficultyKeyChar = Console.ReadKey(true).KeyChar;
                if (difficultyKeyChar == '2')
                {
                    difficulty = 3;
                }
                break;
            case '2':
                Console.Clear();
                Console.WriteLine("Bye!");
                return;
        }
        
        
        var game = new Game(difficulty);
        game.Start();
    }
    
    private static void DisplayMenu()
    {
        Console.Clear();
        Console.WriteLine("Solitaire");

        for (int i = 0; i < menuItems.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {menuItems[i+1]}");
        }
        
    }
    
    private static void DisplayDifficulty()
    {
        Console.Clear();
        Console.WriteLine("Solitaire");

        Console.WriteLine("1. Easy");
        Console.WriteLine("2. Hard");
    }
}