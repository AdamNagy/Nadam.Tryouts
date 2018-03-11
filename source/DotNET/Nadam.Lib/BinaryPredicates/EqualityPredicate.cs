namespace Nadam.Lib
{
    public static partial class Predicates
    {
        public static bool Equality(string a, string b)
        {
            if (a == null || b == null)
                return false;
            return a.Equals(b);
        }

        public static bool Equality(object a, object b)
        {
            if (a == null || b == null)
                return false;
            return a.Equals(b);
        }
    }
}
