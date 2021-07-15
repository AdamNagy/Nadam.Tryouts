using System;
using System.Text;

namespace StringNum
{
    public static class StringExtensions
    {
        public static string Backward(this string toReverse)
        {
            char[] charArray = toReverse.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static Whole ToStringNum(this StringBuilder num)
            => num.ToString().ToStringNum();

        public static Whole ToStringNum(this String num)
        {
            if( num.Contains('.') )
            {
                var splitted = num.Split('.');
                return new Real(splitted[0], splitted[1]);
            }
                
            return new Whole(num);
        }
    }
}
