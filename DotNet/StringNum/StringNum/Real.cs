using System;

namespace StringNum
{
    public class Real : Whole, IComparable
    {
        public Whole Fraction { get; set; }

        #region ctor
        public Real(Whole intPart, Whole fraction) : base(intPart)
        {
            Fraction = String.IsNullOrEmpty(fraction.Number) ? Zero : fraction;
        }

        public Real(string intPart, string fraction) : base(intPart)
        {
            Fraction = new Whole(fraction);
        }

        public Real(Whole intPart) : base(intPart)
        {
            Fraction = Zero;
        }
        #endregion

        #region utils
        public override string ToString()
        {
            if( Fraction == Zero )
                return $"{base.ToString()}";

            return $"{base.ToString()}.{Fraction}";
        }

        public override int CompareTo(object obj)
        {
            var other = (Real)obj;
            if (this > other)
                return 1;

            if (this < other)
                return -1;

            return 0;

        }
        #endregion
    }
}
