using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringNum
{
    public class Whole : IComparable
    {
        public string Number { get; private set; }
        public int Length { get => Number.Length; }
        public bool IsNegative { get; private set; }

        #region ctor
        public Whole(string number)
        {
            if (number.StartsWith('-'))
            {
                IsNegative = true;
                Number = number.TrimStart('-');
            }
            else
                Number = number;
        }

        public Whole(Whole num)
        {
            Number = num.Number;
            IsNegative = num.IsNegative;
        }
        #endregion

        #region basic arithmetic operator
        public static Whole operator +(Whole aNum, Whole bNum)
        {
            if (bNum.IsNegative)
                return aNum - bNum.Abs();
            else if( aNum.IsNegative )
            {
                if (aNum.Abs() > bNum.Abs())
                {
                    var res = aNum.Abs() - bNum;
                    return res * Whole.MinusOne;
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
                return new Whole($"{pre.Backward()}{stringNumBuilder.ToStringNum().Backward()}");
            }

            return stringNumBuilder.ToStringNum().Backward();
        }

        public static Whole operator -(Whole a, Whole b)
        {
            if (b.IsNegative)
                return a + b.Abs();

            if( a.IsNegative )
            {
                var result = a.Abs() + b;
                return result * Whole.MinusOne;
            }

            if( b > a )
            {
                var result = b - a;
                return result * Whole.MinusOne;
            }

            if (a == b)
                return Whole.Zero;

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
                return new Whole($"-{stringNumBuilder.ToString().Backward().TrimStart('0')}");

            return new Whole(stringNumBuilder.ToString().Backward().TrimStart('0'));
        }

        #region multiply
        public static Whole operator *(Whole a, Whole b)
        {
            if (a == Whole.Zero || b == Whole.Zero)
                return new Whole("0");

            if (a == Whole.MinusOne)
                return new Whole($"-{b.Number}");

            if (b == Whole.MinusOne)
                return new Whole($"-{a.Number}");

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
                return new Whole($"-{sumBuilder.Number}");

            return sumBuilder;
        }

        public static Whole operator *(Whole a, int b)
            => a * new Whole(b.ToString());

        public static Whole operator *(int a, Whole b)
            => new Whole(a.ToString()) * b;
        #endregion

        public static Real operator /(Whole a, Whole b)
        {
            if (b == Zero)
                throw new DivideByZeroException();

            if (a == Zero)
                return (Real)Zero;

            var dividence = Divide(a, b);
            var whole = dividence.whole;

            var fractionBuilder = new StringBuilder();
            int digits = 0;
            while (dividence.fraction > Zero && 
                StringNumOptions.DefaultNumberOfFraction > digits)
            {
                dividence = Divide(dividence.fraction * 10, b);
                fractionBuilder.Append(dividence.whole);
                ++digits;
            }

            return new Real(whole, new Whole(fractionBuilder.ToString()));
        }

        private static (Whole whole, Whole fraction) Divide(Whole a, Whole b)
        {
            var wholes = Whole.Zero;
            while (a > b || a == b)
            {
                ++wholes;
                a -= b;
            }

            return (wholes, a);
        }
        #endregion

        #region comparsion operators
        public static bool operator ==(Whole a, Whole b)
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

        public static bool operator !=(Whole a, Whole b)
            => a.Length != b.Length || a != b;

        public static bool operator <(Whole a, Whole b)
        {
            var lt = a.Abs().Lt(b.Abs());

            if (a.IsNegative && !b.IsNegative)
                return true;

            if (!a.IsNegative && b.IsNegative)
                return false;

            return lt;
        }

        public static bool operator >(Whole a, Whole b)
        {
            var lt = a.Abs().Gt(b.Abs());

            if (!a.IsNegative && b.IsNegative)
                return true;

            if (a.IsNegative && !b.IsNegative)
                return false;

            return lt;
        }
        #endregion

        #region unary operator
        public static Whole operator ++(Whole a)
            => a + Whole.One;

        public static Whole operator --(Whole a)
            => a - Whole.One;
        #endregion

        #region math functions
        public Whole Abs()
            => new Whole(Number);
        #endregion

        #region helpers
        public char this[int i]
        {
            get => Number[i];
        }

        private bool Lt(Whole other)
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

        private bool Gt(Whole other)
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

        private Whole Backward()
        {
            char[] charArray = Number.ToCharArray();
            Array.Reverse(charArray);
            return new Whole(new string(charArray));
        }

        public int[] Digits()
        {
            var intArr = new int[Number.Length];
            for (int i = 0; i < Number.Length; i++)
                intArr[i] = (int)char.GetNumericValue(Number[i]);

            return intArr;
        }
        #endregion

        #region utils
        public CharEnumerator GetEnumerator()
            => Number.GetEnumerator();

        public override string ToString()
        {
            if (IsNegative)
                return $"-{Number}";

            return Number;
        }

        public virtual int CompareTo(object obj)
        {
            var other = (Whole)obj;
            if (this > other)
                return 1;

            if (this < other)
                return -1;

            return 0;

        }

        public override bool Equals(object obj)
            => this == (Whole)obj;

        public override int GetHashCode()
            => ToString().GetHashCode();
        #endregion

        #region static const
        public static Whole Zero { get => new Whole("0"); }
        public static Whole One { get => new Whole("1"); }
        public static Whole MinusOne { get => new Whole("-1"); }
        #endregion
    }
}
