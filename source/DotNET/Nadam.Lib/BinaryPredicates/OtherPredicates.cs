namespace Nadam.Lib
{
    public static partial class Predicates_
    {
        public static bool NoFilter(object a, object b)
        {
            return true;
        }
    }

    public enum Predicates
    {
        Equality,
        AntiEquality,
        GreaterThan,
        LessThan,
        IsOdd,
        IsEven
    }
}
