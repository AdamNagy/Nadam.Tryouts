using System;

namespace Nadam.Lib
{
    public enum PredicatesType
    {
        IsEqualTo,
        IsNotEqualTo,
        IsGreaterThan,
        IsLessThan,
    }

    public static class PredicatesLib
    {
        #region Less than predicates
        public static bool LessThanPredicate(int x, int y)
        {
            return x < y;
        }

        public static bool LessThanPredicate(double x, double y)
        {
            return x < y;
        }

        public static bool LessThanPredicate(float x, float y)
        {
            return x < y;
        }

        public static bool LessThanPredicate(string x, string y)
        {
            if (x == null || y == null)
                throw new ArgumentException("Predicates can not operates on null with string operands");

            try
            {
                return Convert.ToInt32(x) < Convert.ToInt32(y);
            }
            catch (FormatException formEx)
            {
                throw new FormatException("Predicates intented to work with string that contains whole! number only.");
            }
        }

        public static bool LessThanPredicate(DateTime x, DateTime y)
        {
            return x < y;
        }

        public static bool LessThanPredicate(object x, object y)
        {
            if (x == null || y == null)
                throw new ArgumentException("Predicates can not operates on null compley types (objects)");
            try
            {
                return Convert.ToInt32(x) < Convert.ToInt32(y);
            }
            catch (FormatException formEx)
            {
                throw new FormatException("Predicates intented to work with string that contains number.");
            }


        }
        #endregion

        #region Greater than predicates
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
        #endregion

        #region Equality predicates
        public static bool EqualityPredicate(object a, object b)
        {
            if (a == null)
                return false;
            return a.Equals(b);
        }
        #endregion

        #region Anty equality predicates
        #endregion

        #region Other predicated

        public static bool NoFilter(object a, object b)
        {
            return true;
        }
        #endregion
    }
}
