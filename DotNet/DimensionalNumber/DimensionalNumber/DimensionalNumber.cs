namespace DimensionalNumberLib
{
    public class DimensionalNumber
    {
        public Double Value { get; private set; }
        public Measurement Mesurement { get; private set; }
        public Prefix Prefix { get; private set; }
        public int Dimension { get; private set; }

        public DimensionalNumber(Double value, Measurement type) : this(value, type, Prefix.One)
        {
            Dimension = 1;
        }

        public DimensionalNumber(Double value, Measurement type, Prefix prefix)
        {
            Value = value;
            Mesurement = type;
            Prefix = prefix;

        }

        public DimensionalNumber(string value)
        {
            // parse string
        }

        public DimensionalNumber ChangePrefix(Prefix prefix)
        {
            var normalized = NormalizePrefix();

            double newValue = 0;
            if (prefix.Value > 1)
            {
                newValue = normalized.Value * prefix.Value;
            }
            else
            {
                newValue = normalized.Value / prefix.Value;
            }

            return new DimensionalNumber(newValue, Mesurement, prefix);
        }

        public DimensionalNumber NormalizePrefix()
        {
            if (Prefix.Order == 1) return this;

            Double newValue = 0;
            if (Prefix.Order < 1)
            {
                newValue = Value / Prefix.Value;
            }
            else
            {
                newValue = Value * Prefix.Value;
            }

            return new DimensionalNumber(newValue, Mesurement, Prefix.One);
        }


        #region addition
        //public static DimensionalNumber operator +(DimensionalNumber a, DimensionalNumber b)
        //{

        //}

        #endregion

        public override string ToString()
            => $"{Value} {Prefix.Label}{Mesurement.Unit}";
    }


}
