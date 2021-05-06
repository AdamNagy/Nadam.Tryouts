using System;
using Xunit;
using Dimensional2;

namespace DimensionalTests
{
    public class UnitTest1
    {
        [Fact]
        public void DimensionalNumShouldBeCreated()
        {
            var a = new Dimensional(5, Dimensions.Length, "cm");
            Assert.Equal(5, a.Value);
        }

        [Fact]
        public void AssertForToString()
        {
            var a = new Dimensional(5, Dimensions.Length, "cm");
            Assert.Equal("5cm", a.ToString());
        }

        [Fact]
        public void WrongDimension()
        {
            Assert.Throws<ArgumentException>(() => new Dimensional(5, Dimensions.Length, "ck"));
        }

        [Fact]
        public void WrongDimensionType()
        {
            Assert.Throws<ArgumentException>(() => new Dimensional(5, Dimensions.Length, "ck"));
        }

        [Fact]
        public void AsserPrefixName()
        {
            var a = new Dimensional(5, Dimensions.Length, "km");
            Assert.Equal("kilo", a.Prefix.Name);
        }

        [Fact]
        public void AsserPrefixSName()
        {
            var a = new Dimensional(5, Dimensions.Length, "km");
            Assert.Equal("k", a.Prefix.SName);
        }

        [Fact]
        public void Normalize1()
        {
            var a = new Dimensional(5, Dimensions.Length, "km");
            a.Normalize();

            Assert.Equal(5000, a.Value);
        }

        [Fact]
        public void Normalize2()
        {
            var a = new Dimensional(5, Dimensions.Length, "Mm");
            a.Normalize();

            Assert.Equal(5000000, a.Value);
        }

        [Fact]
        public void Normalize3()
        {
            var a = new Dimensional(50, Dimensions.Length, "cm");
            a.Normalize();

            Assert.Equal(0.5, a.Value);
        }

        [Fact]
        public void Normalize4()
        {
            var a = new Dimensional(5000, Dimensions.Length, "mm");
            a.Normalize();

            Assert.Equal(5, a.Value);
        }

        [Fact]
        public void ToOTherPrefix1_Bigger()
        {
            var a = new Dimensional(5000, Dimensions.Length, "mm");
            a.ToOtherPrefix("c");

            Assert.Equal(500, a.Value);
        }

        [Fact]
        public void ToOTherPrefix1_Smaller()
        {
            var a = new Dimensional(5, Dimensions.Length, "cm");
            a.ToOtherPrefix("m");

            Assert.Equal(50, a.Value);
        }

        [Fact]
        public void Addition1()
        {
            var a = new Dimensional(5, Dimensions.Length, "m");
            var b = new Dimensional(3, Dimensions.Length, "m");

            var c = a + b;

            Assert.Equal(8, c.Value);
        }

        [Fact]
        public void Addition2()
        {
            var a = new Dimensional(5, Dimensions.Length, "cm");
            var b = new Dimensional(3, Dimensions.Length, "cm");

            var c = a + b;

            Assert.Equal(8, c.Value);
        }

        [Fact]
        public void Addition3()
        {
            var a = new Dimensional(2, Dimensions.Length, "m");
            var b = new Dimensional(30, Dimensions.Length, "cm");

            var c = a + b;

            Assert.Equal(2.3, c.Value);
        }

        [Fact]
        public void Addition4()
        {
            var a = new Dimensional(2, Dimensions.Length, "m");
            var b = new Dimensional(4, Dimensions.Length, "dm");

            var c = a + b;

            Assert.Equal(24, c.Value);
        }

        [Fact]
        public void Addition5()
        {
            var a = new Dimensional(20, Dimensions.Length, "cm");
            var b = new Dimensional(10, Dimensions.Length, "mm");

            var c = a + b;

            Assert.Equal(210, c.Value);
        }

        [Fact]
        public void Addition6()
        {
            var a = new Dimensional(20, Dimensions.Length, "mm");
            var b = new Dimensional(10, Dimensions.Length, "dm");

            var c = a + b;

            Assert.Equal(1020, c.Value);
        }

        [Fact]
        public void Currency()
        {
            var a = new Dimensional(20000, Dimensions.Currency, "huf");

            Assert.Equal("20000huf", a.ToString());
        }

        [Fact]
        public void CurrencyToOtherPrefix()
        {
            var a = new Dimensional(20000, Dimensions.Currency, "huf");
            a.ToOtherPrefix("usd");

            Assert.Equal("20000huf", a.ToString());
        }
    }
}
