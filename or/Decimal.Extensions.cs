namespace Math;

public static class Decimal
{
    public static bool EqualsZeroEPS(this decimal x)
        => Consts.MinusEPS < x && x < Consts.EPS;

    public static bool LessThanZeroEPS(this decimal x)
        => x < Consts.MinusEPS;

    public static bool LessOrEqualsZeroEPS(this decimal x)
        => x < Consts.EPS;

    public static bool GreaterThanZeroEPS(this decimal x)
        => x > Consts.EPS;

    public static bool GreaterOrEqualsZeroEPS(this decimal x)
        => x > Consts.MinusEPS;

    public static bool IsIntegerEPS(this decimal x)
        => System.Math.Abs(x - System.Math.Round(x)) < Consts.EPS;

    public static decimal GetRealPart(this decimal x)
        => (x - System.Math.Truncate(x)).IsIntegerEPS() ? 0 : (x - System.Math.Floor(x));
}
