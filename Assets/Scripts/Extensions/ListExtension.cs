using System;
using System.Collections.Generic;

public static class ListExtension
{
    private static Random rng = new Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    /// <summary>
    /// Split a list in half, with a count bias to the first returned list if uneven
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static (List<T>, List<T>) SplitInHalf<T>(this List<T> source)
    {
        int firstsize = (int)Math.Ceiling(source.Count / 2f);
        int secondsize = (int)Math.Floor(source.Count / 2f);

        var first = source.GetRange(0, firstsize);
        var second = source.GetRange(firstsize, secondsize);

        return (first, second);
    }
}
