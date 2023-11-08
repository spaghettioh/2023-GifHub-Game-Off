using System.Linq;

public static class ArrayUtilities
{
    public static T[] Randomize<T>(this T[] things)
    {
        var items = things.ToList();
        var randomize = new T[things.Length];
        var index = 0;
        foreach (var _ in things)
        {
            var randomItem = items.Random();
            randomize[index] = randomItem;
            index++;
            items.Remove(randomItem);
        }

        return randomize;
    }

    public static T Random<T>(this T[] things) =>
        things.Length > 0 ? things[Util.Random(0, things.Length)] : default;
}
