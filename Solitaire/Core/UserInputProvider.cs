namespace Solitaire.Core;

public static class UserInputProvider
{
    public static char Get()
    {
        return Console.ReadKey().KeyChar;
    }
}
