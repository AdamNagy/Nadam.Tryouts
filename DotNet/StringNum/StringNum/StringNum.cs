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
            else if( aNum.IsNegative )
            {
                if (aNum.Abs() > bNum.Abs())
                {
                    var res = aNum.Abs() - bNum;
                    return res * StringNum.MinusOne;
                }
                else
                    return bNum - aNum.Abs();
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
            if (b.IsNegative)
                return a + b.Abs();

            if( a.IsNegative )
            {
                var result = a.Abs() + b;
                return result * StringNum.MinusOne;
            }

            if( b > a )
            {
                var result = b - a;
                return result * StringNum.MinusOne;
            }

            if (a == b)
                return StringNum.Zero;

            var stringNumBuilder = new StringBuilder();
            bool isNegative = false;

            if( a.Length < b.Length )
            {
                var temp = a;
                a = b;
                b = temp;

                isNegative = true;
            }

            int[] aDigits = a.Digits().Reverse().ToArray(),
                bDigits = b.Digits().Reverse().ToArray();

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
                partialSum = current - decrementer;

                stringNumBuilder.Append(partialSum);
            }

            if( isNegative )
                return new StringNum($"-{stringNumBuilder.ToString().Backward().TrimStart('0')}");

            return new StringNum(stringNumBuilder.ToString().Backward().TrimStart('0'));
        }

        public static StringNum operator *(StringNum a, StringNum b)
        {
            if (a == StringNum.Zero || b == StringNum.Zero)
                return new StringNum("0");

            if (a == StringNum.MinusOne)
                return new StringNum($"-{b.Number}");

            if (b == StringNum.MinusOne)
                return new StringNum($"-{a.Number}");

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

            if( (a.IsNegative && !b.IsNegative) || (!a.IsNegative && b.IsNegative) )
                return new StringNum($"-{sumBuilder.Number}");

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
        {
            if( a.Length == b.Length && a.IsNegative == b.IsNegative)
            {
                for (int i = 0; i < a.Number.Length; i++)
                {
                    if (a[i] != b[i])
                        return false;
                }

                return true;
            }

            return false;
        }

        public static bool operator !=(StringNum a, StringNum b)
            => a.Length != b.Length || a != b;

        public static bool operator <(StringNum a, StringNum b)
        {
            var lt = a.Abs().Lt(b.Abs());

            if (a.IsNegative && !b.IsNegative)
                return true;

            if (!a.IsNegative && b.IsNegative)
                return false;

            return lt;
        }

        public static bool operator >(StringNum a, StringNum b)
        {
            var lt = a.Abs().Gt(b.Abs());

            if (!a.IsNegative && b.IsNegative)
                return true;

            if (a.IsNegative && !b.IsNegative)
                return false;

            return lt;
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

        private bool Lt(StringNum other)
        {
            if (Length < other.Length)
                return true;
            else if (Length > other.Length)
                return false;

            int[] aDigits = this.Digits(),
                    bDigits = other.Digits();

            for (int i = 0; i < Length; i++)
            {
                if (aDigits[i] < bDigits[i])
                    return true;
                else if (aDigits[i] > bDigits[i])
                    return false;
            }

            return false;
        }

        private bool Gt(StringNum other)
        {
            if (Length > other.Length)
                return true;
            
            if (Length < other.Length)
                return false;

            int[] aDigits = this.Digits(),
                    bDigits = other.Digits();

            for (int i = 0; i < Length; i++)
            {
                if (aDigits[i] > bDigits[i])
                    return true;
                else if (aDigits[i] < bDigits[i])
                    return false;
            }

            return false;
        }

        private StringNum Backward()
        {
            char[] charArray = Number.ToCharArray();
            Array.Reverse(charArray);
            return new StringNum(new string(charArray));
        }

        public int[] Digits()
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
            if (this > other)
                return 1;

            if (this < other)
                return -1;

            return 0;

        }

        public override bool Equals(object obj)
            => this == (StringNum)obj;        

        public override int GetHashCode()        
            => ToString().GetHashCode();        
        #endregion

        #region static const
        public static StringNum Zero { get => new StringNum("0"); }
        public static StringNum One { get => new StringNum("1"); }
        public static StringNum MinusOne { get => new StringNum("-1"); }
        #endregion
    }
}
