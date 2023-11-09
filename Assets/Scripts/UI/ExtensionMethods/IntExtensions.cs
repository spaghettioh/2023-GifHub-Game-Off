public static class IntExtensions
{
    public static string ToRomanNumeral(this int level)
    {
        const int min = 0;
        const int max = 50;
        if (level is < min or > max)
            throw new (
                $"Input level ({level}) outside compatible range: {min}-{max}"
            );
        return level switch
        {
            < 1 => string.Empty,
            >= 50 => "L" + ToRomanNumeral(level - 50),
            >= 40 => "XL" + ToRomanNumeral(level - 40),
            >= 10 => "X" + ToRomanNumeral(level - 10),
            >= 9 => "IX" + ToRomanNumeral(level - 9),
            >= 5 => "V" + ToRomanNumeral(level - 5),
            >= 4 => "IV" + ToRomanNumeral(level - 4),
            >= 1 => "I" + ToRomanNumeral(level - 1),
        };
    }
}