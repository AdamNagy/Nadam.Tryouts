using System;

namespace Nadam.Global.Lib
{
    public static partial class BinaryPredicates
    {
        #region Less than predicates
        public static bool LessThan(int x, int y)
        {
            return x < y;
        }

        public static bool LessThan(double x, double y)
        {
            return x < y;
        }

        public static bool LessThan(float x, float y)
        {
            return x < y;
        }

        public static bool LessThan(decimal x, decimal y)
        {
            return x < y;
        }

        public static bool LessThan(DateTime x, DateTime y)
        {
            return x < y;
        }

        // TODO: add smart string comparer
        public static bool LessThan(string x, string y)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Greater than predicates
        public static bool GreaterThan(int x, int y)
        {
            return x > y;
        }

        public static bool GreaterThan(double x, double y)
        {
            return x > y;
        }

        public static bool GreaterThan(float x, float y)
        {
            return x > y;
        }


        public static bool GreaterThan(DateTime x, DateTime y)
        {
            return x > y;
        }

        // TODO: add smart string comparer
        #endregion

        #region Equality predicates
        public static bool Equality(int a, int b)
        {
            return a.Equals(b);
        }

        public static bool Equality(float a, float b)
        {
            return a.Equals(b);
        }

        public static bool Equality(double a, double b)
        {
            return a.Equals(b);
        }

        public static bool Equality(decimal a, decimal b)
        {
            return a.Equals(b);
        }

        public static bool Equality(string a, string b)
        {
            return a.Equals(b);
        }

        public static bool Equality(DateTime a, DateTime b)
        {
            if (a == null)
                return false;
            return a.Equals(b);
        }

        public static bool Equality(object a, object b)
        {
            if (a == null)
                return false;
            return a.Equals(b);
        }
        #endregion

        #region Other predicated
        public static bool NoFilter(object a, object b)
        {
            return true;
        }
        #endregion
    }

    public enum PredicatesType
    {
        Equality,
        AntiEquality,
        GreaterThan,
        LessThan,
        NoFilter
    }
}
