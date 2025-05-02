using Solitaire.Core;

namespace Solitaire;

internal static class Program
{
    internal static void Main(string[] args)
    {
        
        
        int difficulty = 1;
        
        difficulty = Console.ReadKey().KeyChar == '1' ? 1 : 3;
        var game = new Game(difficulty);
        game.Start();
    }
}