using Solitaire.Core;
using Solitaire.Core.Models;

namespace Solitaire;

internal static class Program
{
    internal static void Main(string[] args)
    {
        var game = new Game();
        
        game.Start();
    }
}