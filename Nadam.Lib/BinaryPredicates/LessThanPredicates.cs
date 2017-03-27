using System;

namespace Nadam.Lib
{
    public static partial class Predicates
    {
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

        //public static bool LessThanPredicate(string x, string y)
        //{
        //    if (x == null || y == null)
        //        throw new ArgumentException("Predicates can not operates on null with string operands");

        //    try
        //    {
        //        return Convert.ToInt32(x) < Convert.ToInt32(y);
        //    }
        //    catch (FormatException formEx)
        //    {
        //        throw new FormatException("Predicates intented to work with string that contains whole! number only.");
        //    }
        //}

        //public static bool LessThanPredicate(object x, object y)
        //{
        //    if (x == null || y == null)
        //        throw new ArgumentException("Predicates can not operates on null compley types (objects)");
        //    try
        //    {
        //        return Convert.ToInt32(x) < Convert.ToInt32(y);
        //    }
        //    catch (FormatException formEx)
        //    {
        //        throw new FormatException("Predicates intented to work with string that contains number.");
        //    }


        //}
    }
}
