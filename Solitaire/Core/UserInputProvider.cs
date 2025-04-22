namespace Solitaire.Core;

public static class UserInputProvider
{
    public static char Get()
    {
        Console.ForegroundColor = ConsoleColor.White;
        return Console.ReadKey().KeyChar;
    }
}
