using System;

namespace StringNum
{
    public class Digit
    {
        public static (char ones, char tens) Add(char a, char b)
        {
            var numA = Char.GetNumericValue(a);
            var numB = Char.GetNumericValue(b);

            var numSum = numA + numB;
            var sum = numSum.ToString();

            if( numSum >= 10 )
            {
                return (sum[1], sum[0]);
            } 
            else
            {
                return (sum[0], '0');
            }
        }

        public static (char ones, char tens) Multiply(char a, char b)
        {
            var numA = Char.GetNumericValue(a);
            var numB = Char.GetNumericValue(b);

            var numSum = numA * numB;
            var sum = numSum.ToString();

            if (numSum >= 10)
            {
                return (sum[1], sum[0]);
            }
            else
            {
                return (sum[0], '0');
            }
        }
    }
}
