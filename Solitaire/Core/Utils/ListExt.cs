namespace Solitaire.Core.Utils;

public static class ListExt
{
    public static void Shuffle<T>(this IList<T> list)
    {
        var random = new Random();
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            (list[i], list[j]) = (list[j], list[i]); // swap
        }
    }
}
