using System;

namespace Nadam.Lib
{
    public static partial class Predicates
    {
        public static bool GreaterThanPredicate(int x, int y)
        {
            return x > y;
        }

        public static bool GreaterThanPredicate(double x, double y)
        {
            return x > y;
        }

        public static bool GreaterThanPredicate(float x, float y)
        {
            return x > y;
        }

        public static bool GreaterThanPredicate(string x, string y)
        {
            return Convert.ToInt32(x) > Convert.ToInt32(y);
        }

        public static bool GreaterThanPredicate(DateTime x, DateTime y)
        {
            return x > y;
        }

        public static bool GreaterThanPredicate(object x, object y)
        {
            return Convert.ToInt32(x) > Convert.ToInt32(y);
        }
    }
}
