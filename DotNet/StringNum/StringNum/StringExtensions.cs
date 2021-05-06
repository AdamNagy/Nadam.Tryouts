using System;
using System.Text;

namespace StringNumSet
{
    public static class StringExtensions
    {
        public static string Backward(this string toReverse)
        {
            char[] charArray = toReverse.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static int[] ToIntArray(this string stringNum)
        {
            var intArr = new int[stringNum.Length];
            for (int i = 0; i < stringNum.Length; i++)
                intArr[i] = (int)char.GetNumericValue(stringNum[i]);

            return intArr;
        }

        public static StringNum ToStringNum(this StringBuilder num)
            => new StringNum(num.ToString());

        public static StringNum ToStringNum(this String num)
                => new StringNum(num);
    }
}
