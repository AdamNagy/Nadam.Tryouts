using DimensionalNumberLib;

namespace DimensionalNumberLibTests
{
    public class NormalizePrefixTests
    {
        [Fact]
        public void Cento()
        {
            var dimNum = DimensionalNumberFactory.Length.Create(100, Prefix.Cento);

            var result = dimNum.NormalizePrefix();
            var stringified = result.ToString();

            Assert.Equal("1 m", stringified);
        }

        [Fact]
        public void Milli()
        {
            var dimNum = DimensionalNumberFactory.Length.Create(1000, Prefix.Milli);

            var result = dimNum.NormalizePrefix();
            var stringified = result.ToString();

            Assert.Equal("1 m", stringified);
        }

        [Fact]
        public void Micro()
        {
            var dimNum = DimensionalNumberFactory.Length.Create(1000000, Prefix.Micro);

            var result = dimNum.NormalizePrefix();
            var stringified = result.ToString();

            Assert.Equal("1 m", stringified);
        }

        [Fact]
        public void Cento_less_than_one()
        {
            var dimNum = DimensionalNumberFactory.Length.Create(50, Prefix.Cento);

            var result = dimNum.NormalizePrefix();
            var stringified = result.ToString();

            Assert.Equal("0.5 m", stringified);
        }

        [Fact]
        public void Milli_less_than_one()
        {
            var dimNum = DimensionalNumberFactory.Length.Create(628, Prefix.Milli);

            var result = dimNum.NormalizePrefix();
            var stringified = result.ToString();

            Assert.Equal("0.628 m", stringified);
        }

        [Fact]
        public void Kilo()
        {
            var dimNum = DimensionalNumberFactory.Length.Create(1, Prefix.Kilo);

            var result = dimNum.NormalizePrefix();
            var stringified = result.ToString();

            Assert.Equal("1000 m", stringified);
        }

        [Fact]
        public void Kilo_2()
        {
            var dimNum = DimensionalNumberFactory.Length.Create(1.23, Prefix.Kilo);

            var result = dimNum.NormalizePrefix();
            var stringified = result.ToString();

            Assert.Equal("1230 m", stringified);
        }

        [Fact]
        public void Mega()
        {
            var dimNum = DimensionalNumberFactory.Length.Create(1, Prefix.Mega);

            var result = dimNum.NormalizePrefix();
            var stringified = result.ToString();

            Assert.Equal("1000000 m", stringified);
        }

        [Fact]
        public void Mega_2()
        {
            var dimNum = DimensionalNumberFactory.Length.Create(1.23, Prefix.Mega);

            var result = dimNum.NormalizePrefix();
            var stringified = result.ToString();

            Assert.Equal("1230000 m", stringified);
        }
    }
}
