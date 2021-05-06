using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringNumSet
{
    public class StringNum : IComparable
    {
        public string Number { get; private set; }
        public int Length { get => Number.Length; }
        public bool IsNegative { get; private set; }

        public StringNum(string number)
        {
            if (number.StartsWith('-'))
            {
                IsNegative = true;
                Number = number.TrimStart('-');
            }
            else
                Number = number;
        }

        #region basic arithmetic operator
        public static StringNum operator +(StringNum aNum, StringNum bNum)
        {
            if (bNum.IsNegative)
                return aNum - bNum.Abs();

            if (aNum.IsNegative)
            {
                if( bNum.Abs() > aNum.Abs )

                return new StringNum(aNum.Number) - bNum;
            }

            var a = aNum.Number.Backward();
            var b = bNum.Number.Backward();

            var stringNumBuilder = new StringBuilder();

            var baseNum = a.Length <= b.Length ? a : b;
            var fraction = '0';

            for (int i = 0; i < baseNum.Length; i++)
            {
                var s = Digit.Add(a[i], b[i]);
                var newFraction = s.tens;
                s = Digit.Add(fraction, s.ones);
                stringNumBuilder.Append(s.ones);

                fraction = newFraction;
            }

            if( fraction != '0' )
                stringNumBuilder.Append(fraction);

            if ( a.Length != b.Length )
            {
                var longer = a.Length <= b.Length ? b : a;
                var pre = longer.Substring(baseNum.Length);
                return new StringNum($"{pre.Backward()}{stringNumBuilder.ToStringNum().Backward()}");
            }

            return stringNumBuilder.ToStringNum().Backward();
        }

        public static StringNum operator -(StringNum a, StringNum b)
        {
            //if (b.IsNegative)
            //    return a + new StringNum(b.Number);

            var stringNumBuilder = new StringBuilder();
            bool isNegative = false;

            if( a.Length < b.Length )
            {
                var temp = a;
                a = b;
                b = temp;

                isNegative = true;
            }

            int[] aDigits = a.ToIntArray().Reverse().ToArray(),
                bDigits = b.ToIntArray().Reverse().ToArray();

            for (int i = 0; i < aDigits.Length; i++)
            {
                var current = aDigits[i];
                var decrementer = i < bDigits.Length ? bDigits[i] : 0;

                if (current < decrementer)
                {
                    current = Convert.ToInt32($"1{current}");

                    if( aDigits.Length > i + 1 )
                        aDigits[i+1]--;
                }

                int partialSum;
                if( a.IsNegative )
                    partialSum = (current * -1) + decrementer;
                else
                    partialSum = current - decrementer;

                stringNumBuilder.Append(partialSum);
            }

            if( isNegative )
                return new StringNum($"-{stringNumBuilder.ToString().Backward()}");

            return new StringNum(stringNumBuilder.ToString().Backward());
        }

        public static StringNum operator *(StringNum a, StringNum b)
        {
            if (a == '0' || b == '0')
                return new StringNum("0");

            var fraction = '0';
            var digitalSum = new List<string>();
            var iteration = 0;
            foreach (var digitA in b)
            {
                var stringNumBuilder = new StringBuilder();
                foreach (var digitB in a.Backward())
                {
                    var digitMultiply = Digit.Multiply(digitA, digitB);
                    var newFraction = digitMultiply.tens;
                    digitMultiply = Digit.Add(fraction, digitMultiply.ones);
                    stringNumBuilder.Append(digitMultiply.ones);

                    fraction = newFraction;
                }

                if (fraction != '0')
                    stringNumBuilder.Append(fraction);

                stringNumBuilder = new StringBuilder(stringNumBuilder.ToString().Backward());
                var shiftLeft = b.Length - (++iteration);
                for (int i = 0; i < shiftLeft; i++)                
                    stringNumBuilder.Append("0");
                
                digitalSum.Add((stringNumBuilder.ToString()));
            }

            var sumBuilder = "0".ToStringNum();
            foreach (var subNum in digitalSum)            
                sumBuilder = sumBuilder + subNum.ToStringNum();            

            return sumBuilder;
        }

        //public static (string whole, string fraction) Devide(string a, string b, int numOfDigits = 4)
        //{
        //    if (a == "0" || b == "0")
        //        return ("/", "/");

        //    var devidence = Devide(numA, numB);
        //    int whole = devidence.whole;

        //    var fractionBuilder = new StringBuilder();
        //    int digits = 0;
        //    while (devidence.fraction > 0 && numOfDigits > digits)
        //    {
        //        devidence = Devide(devidence.fraction * 10, numB);
        //        fractionBuilder.Append(devidence.whole);
        //        ++digits;
        //    }

        //    return (whole.ToString(), fractionBuilder.ToString());
        //}

        //private static (int whole, int fraction) DevideOnce(string a, string b)
        //{
        //    var wholes = 0;
        //    while (numA >= numB)
        //    {
        //        ++wholes;
        //        numA -= numB;
        //    }

        //    return (wholes, numA);
        //}
        #endregion

        #region comparsion operators
        public static bool operator ==(StringNum a, StringNum b)
            => a.Length == b.Length && a == b;

        public static bool operator !=(StringNum a, StringNum b)
            => a.Length != b.Length || a != b;

        public static bool operator ==(StringNum a, char b)
            => a.Length == 1 && a.Number[0] == b;        

        public static bool operator !=(StringNum a, char b)
            => a.Length != 1 || a.Number[0] != b;

        public static bool operator <(StringNum a, StringNum b)
        {
            if (a.IsNegative && !b.IsNegative)
                return true;

            if (a.Length < b.Length)
                return true;

            for (int i = 0; i < a.Length; i++)
            {

            }
        }

        public static bool operator >(StringNum a, StringNum b)
        {

        }
        #endregion

        #region math functions
        public StringNum Abs()
            => new StringNum(Number);
        #endregion

        #region helpers
        public char this[int i]
        {
            get => Number[i];
        }

        public StringNum Substring(int start, int length)
            => new StringNum(Number.Substring(start, length));

        public StringNum Substring(int start)
             => new StringNum(Number.Substring(start));

        private StringNum Backward()
        {
            char[] charArray = Number.ToCharArray();
            Array.Reverse(charArray);
            return new StringNum(new string(charArray));
        }

        public int[] ToIntArray()
        {
            var intArr = new int[Number.Length];
            for (int i = 0; i < Number.Length; i++)
                intArr[i] = (int)char.GetNumericValue(Number[i]);

            return intArr;
        }

        public CharEnumerator GetEnumerator()
            => Number.GetEnumerator();

        public override string ToString()
        {
            if (IsNegative)
                return $"-{Number}";

            return Number;
        }
            

        public int CompareTo(object obj)
        {
            var other = (StringNum)obj;
            return String.Compare(this.Number, other.Number);
        }
        #endregion
    }
}
