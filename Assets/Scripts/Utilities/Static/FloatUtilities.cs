public static class FloatUtilities
{
    public static float RandomAround(this float input, float radius) =>
        Util.Random(input - radius, input + radius);

    public static float RandomNegativePositive(this float input) =>
        Util.Random(-input, input);
}
