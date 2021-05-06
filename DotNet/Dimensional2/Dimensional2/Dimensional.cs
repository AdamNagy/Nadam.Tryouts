using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimensional2
{
    public struct Prefix
    {
        public string Name { get; private set; }
        public string SName { get; private set; }

        public Double Value { get; private set; }

        public Prefix(string name, string sName, Double value)
        {
            Name = name;
            SName = sName;
            Value = value;
        }
    }

    public static class Prefixes
    {
        public static int BaseLine = 1;
        public static int CurrencyBaseLine = 100;

        public static readonly Prefix[] FractinalTypes = {
            new Prefix("deci", "d", 1e-1),
            new Prefix("centi", "c", 1e-2),
            new Prefix("milli", "m", 1e-3),
            new Prefix("milli", "mi", 1e-6),
            new Prefix("milli", "n", 1e-9),
        };

        public static readonly Prefix[] MultiTypes = {
            new Prefix("kilo", "k", 1e3),
            new Prefix("mega", "M", 1e6),
            new Prefix("giga", "g", 1e9),
        };

        public static Prefix[] CurrencyTypes = {
            new Prefix("hungarian forint", "huf", CurrencyBaseLine),
            new Prefix("USA dollar", "usd", 230),
            new Prefix("euro", "eur", 300),
        };

        public static Prefix GetPrefixFor(string pre)
        {
            Prefix prefix;
            prefix = FractinalTypes.FirstOrDefault(p => p.SName == pre.ToString());

            if (!String.IsNullOrEmpty(prefix.Name))
                return prefix;

            prefix = MultiTypes.FirstOrDefault(p => p.SName == pre.ToString());
            if (!String.IsNullOrEmpty(prefix.Name))
                return prefix;

            throw new ArgumentException($"Can not determine prefix for {pre}");
        }
    }

    public struct Dimension
    {
        public string Name { get; private set; }
        public string SName { get; private set; }

        public int Scale { get; private set; }
        public Prefix Prefix { get; private set; }

        public Dimension(string name, string sName, int scale, Prefix prefix)
        {
            Name = name;
            SName = sName;
            Scale = scale;
            Prefix = prefix;
        }

        public Dimension(string name, string sName, int scale)
        {
            Name = name;
            SName = sName;
            Scale = scale;
            Prefix = new Prefix();
        }
    }

    public static class Dimensions
    {
        public const string Length = "length";
        public const string Weight = "weight";
        public const string Currency = "currency";

        public static readonly Dictionary<string, Dimension> Types = new Dictionary<string, Dimension> {
           [Length] = new Dimension("meter", "m", 1),
           [Weight] = new Dimension("gram", "g", 1),
           [Currency] = new Dimension("currency", "", 1),
        };
    }

    public class Dimensional
    {
        public Double Value { get; private set; }
        public Dimension Dimension { get; set; }
        public Prefix Prefix { get; set; }

        public Dimensional(int val, string dimName, string dim)
        {
            if (!Dimensions.Types.ContainsKey(dimName))
                throw new ArgumentException($"Dimension does not known: {dimName}");

            if (!dim.EndsWith(Dimensions.Types[dimName].SName))
                throw new ArgumentException($"Dimension ({dim}) does not know for dimension ({dimName})");

            Value = val;
            Dimension = Dimensions.Types[dimName]; ;

            if(dimName != "currency" && dim.Length > 1 )
                Prefix = Prefixes.GetPrefixFor(dim[0].ToString());
        }

        public Dimensional(double val, string dimName)
        {
            if (!Dimensions.Types.ContainsKey(dimName))
                throw new ArgumentException($"Dimension does not known: {dimName}");

            Value = val;
            Dimension = Dimensions.Types[dimName];
        }

        public void Normalize()
        {
            if (String.IsNullOrEmpty(Prefix.Name))
                return;

            Value *= Prefix.Value;
            Prefix = new Prefix("", "", 0);
        }

        public void ToOtherPrefix(string prefixSign)
        {
            var prefix = Prefixes.GetPrefixFor(prefixSign);
            Normalize();

            if (prefix.Value < 1)
                Value /= prefix.Value;
            else
                Value *= prefix.Value;

            Prefix = prefix;
        }

        public static Dimensional operator +(Dimensional a, Dimensional b)
        {
            if (a.DimensionName != b.DimensionName)
                throw new ArgumentException($"{a.DimensionName} does not compatible with {b.DimensionName}");

            if (a.Prefix.Value > b.Prefix.Value)
            {
                if (String.IsNullOrEmpty(b.Prefix.SName))
                    a.Normalize();
                else
                    a.ToOtherPrefix(b.Prefix.SName[0].ToString());
            }
            else
            {
                if (String.IsNullOrEmpty(a.Prefix.SName))
                    b.Normalize();
                else
                    b.ToOtherPrefix(a.Prefix.SName[0].ToString());
            }

            return new Dimensional(a.Value + b.Value, a.DimensionName);
        }

        public override string ToString()
        {
            return $"{Value}{Dimension}";
        }
    }
}
