namespace DimensionalNumberLib
{
    public static class PrefixValue
    {
        public static Double Nano { get; } = 1000000000;
        public static Double Micro { get; } = 1000000;
        public static Double Milli { get; } = 1000;
        public static Double Cento { get; } = 100;
        public static Double One { get; } = 1;
        public static Double Kilo { get; } = One * 1000;
        public static Double Mega { get; } = Kilo * 1000;
        public static Double Giga { get; } = Mega * 1000;
    }

    public struct Prefix
    {
        public Double Value { get; private set; }
        public string Label { get; private set; }
        public int Order { get; private set; }

        private Prefix(string label, Double value, int order)
        {
            Value = value;
            Label = label;
            Order = order;
        }

        public static Prefix Nano { get; } = new Prefix("n", PrefixValue.Nano, -4);
        public static Prefix Micro { get; } = new Prefix("mic", PrefixValue.Micro, -3);
        public static Prefix Milli { get; } = new Prefix("m", PrefixValue.Milli, -2);
        public static Prefix Cento { get; } = new Prefix("c", PrefixValue.Cento, -1);

        public static Prefix One { get; } = new Prefix("", PrefixValue.One, 1);

        public static Prefix Kilo { get; } = new Prefix("k", PrefixValue.Kilo, 2);
        public static Prefix Mega { get; } = new Prefix("M", PrefixValue.Mega, 3);
        public static Prefix Giga { get; } = new Prefix("G", PrefixValue.Giga, 4);

        public override string ToString()
            => Label;

        public static bool operator ==(Prefix left, Prefix right)
            => left.Value == right.Value &&
            left.Label == right.Label;

        public static bool operator !=(Prefix left, Prefix right)
            => left.Value != right.Value ||
            left.Label != right.Label;

        public override bool Equals(object obj)
        {
            if (obj is not Prefix) return false;

            return Value == ((Prefix)obj).Value &&
                Label == ((Prefix)obj).Label;
        }

        public override int GetHashCode()
            => $"{Label}{Order}".GetHashCode();
    }
}
