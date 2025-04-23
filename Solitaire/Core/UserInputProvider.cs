namespace Solitaire.Core;

public static class UserInputProvider
{
    public static ConsoleKey Get()
    {
        Console.ForegroundColor = ConsoleColor.White;
        return Console.ReadKey().Key;
    }
}
