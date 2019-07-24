using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CshTryouts
{
    public struct StringNum
    {
        private int[] digits;
        private bool isLessThanZero;

        public StringNum(string _val)
        {
            isLessThanZero = _val.StartsWith("-") ? true : false;

            if (_val.TrimStart(new char[] { '-'}).IsNumber() )
                digits = _val.ToDigits();
            else            
                throw new ArgumentException($"Given string contains non numerical characters: {_val}");
        }

        public StringNum(int from)
        {
            isLessThanZero = from < 0 ? true : false;
            digits = Math.Abs(from).ToDigits();
        }

        public static StringNum operator +(StringNum a, StringNum b)
        {
            var sumDigits = new LinkedList<int>();
            int[] a_reversed = a.digits.Reverse().ToArray(),
                  b_reversed = b.digits.Reverse().ToArray();

            int digitIdx = 0,
                biggerNumberLengh = Math.Max(a_reversed.Length, b_reversed.Length),
                remainder = 0;

            for (; digitIdx < biggerNumberLengh; ++digitIdx)
            {
                int digA = a_reversed.Length > digitIdx ? a_reversed[digitIdx] : 0,
                    digB = b_reversed.Length > digitIdx ? b_reversed[digitIdx] : 0;

                var sum = (digA + digB + remainder).ToDigits();
                remainder = sum.Length > 1 ? sum[0] : 0;

                sumDigits.AddFirst(sum.Length > 1 ? sum[1] : sum[0]);
            }

            if (remainder > 0)
                sumDigits.AddFirst(remainder);

            var builder = new StringBuilder();
            foreach (var digit in sumDigits)
                builder.Append(digit);

            return new StringNum(builder.ToString());
        }

        public static bool operator >(StringNum left, StringNum right)
        {
            if( (!left.isLessThanZero && right.isLessThanZero)
                || !(left.isLessThanZero && right.isLessThanZero) && left.digits.Length > right.digits.Length)
            {
                return true;
            }

            return false;
        }

        public static bool operator <(StringNum left, StringNum right)
        {
            if ((!left.isLessThanZero && right.isLessThanZero)
                || !(left.isLessThanZero && right.isLessThanZero) && left.digits.Length > right.digits.Length)
            {
                return false;
            }

            return true;
        }

        #region Equality operators
        public static bool operator ==(StringNum left, string right)
        {
            if (!right.IsNumber())
                return false;

            var rightAsDigits = right.ToDigits();

            if (left.digits.Length != rightAsDigits.Length)
                return false;

            for (int i = 0; i < left.digits.Length; i++)
            {
                if (left.digits[i] != rightAsDigits[i])
                    return false;
            }

            return true;
        }

        public static bool operator !=(StringNum left, string right)
            => !(left == right);

        public static bool operator ==(string left, StringNum right)
            => right == left;

        public static bool operator !=(string left, StringNum right)
            => !(right == left);

        public static bool operator ==(StringNum left, StringNum right)
        {
            if (left.digits.Length != right.digits.Length)
                return false;

            for (int i = 0; i < left.digits.Length; i++)
            {
                if (left.digits[i] != right.digits[i])
                    return false;
            }

            return true;
        }

        public static bool operator !=(StringNum left, StringNum right)
            => !(left != right);
        #endregion

        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (var digit in digits)
                builder.Append(digit);

            return builder.ToString();
        }
    }

    public static class ExtensionsForStringNumber
    {
        public static int[] ToDigits(this int number)
        {
            if (number == 0)
                return new int[] { 0 };

            var result = new LinkedList<int>();

            var remaining = number;
            while(remaining > 0)
            {
                result.AddFirst(remaining % 10);
                remaining /= 10;
            }

            return result.ToArray();
        }

        public static int[] ToDigits(this string number)
        {
            var result = new List<int>();
            foreach (var digit in number)
                result.Add(digit.FromAsciiToInt());

            return result.ToArray();
        }

        public static int FromAsciiToInt(this char digit)
        {
            if (digit < 48 || digit > 87)
                throw new ArgumentException($"Cannot not convert to integer: {digit}");

            // Ascii 48 = Decimal 0
            // Ascii 57 = Decimal 9
            return digit - 48;
        }

        public static bool IsNumber(this string value)
            => IsNumber(value.ToCharArray());

        public static bool IsNumber(this char[] value)
        {
            foreach (var digit in value)
            {
                if (digit < 48 || digit > 87)
                    return false;
            }

            return true;
        }
    }
}
