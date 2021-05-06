using System;
using System.Text;

namespace StringNumSet
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
    
        //public static (char whole, char fraction, bool isNegative) Substract(char a, char b)
        //{
        //    var numA = Char.GetNumericValue(a);
        //    var numB = Char.GetNumericValue(b);

        //    var numSum = numA - numB;
        //    return (numSum.ToString().Trim('-')[0], '/', numA < numB);
        //}

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

        private static (int whole, int fraction) Devide(int a, int b)
        {
            var numA = a;
            var numB = b;

            var wholes = 0;
            while (numA >= numB)
            {
                ++wholes;
                numA -= numB;
            }

            return (wholes, numA);
        }
    }
}
