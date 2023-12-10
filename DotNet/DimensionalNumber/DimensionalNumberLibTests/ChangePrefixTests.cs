using DimensionalNumberLib;

namespace DimensionalNumberLibTests
{
    public class ChangePrefixTests
    {
        #region cento
        [Fact]
        public void One_to_cento()
        {
            var dimNum1 = DimensionalNumberFactory.Length.Create(1);

            var result = dimNum1.ChangePrefix(Prefix.Cento);
            var stringified = result.ToString();

            Assert.Equal("100 cm", stringified);
        }

        [Fact]
        public void Ten_to_cento()
        {
            var dimNum1 = DimensionalNumberFactory.Length.Create(10);

            var result = dimNum1.ChangePrefix(Prefix.Cento);
            var stringified = result.ToString();

            Assert.Equal("1000 cm", stringified);
        }

        [Fact]
        public void Hundred_to_cento()
        {
            var dimNum1 = DimensionalNumberFactory.Length.Create(100);

            var result = dimNum1.ChangePrefix(Prefix.Cento);
            var stringified = result.ToString();

            Assert.Equal("10000 cm", stringified);
        }

        [Fact]
        public void Cento_to_one()
        {
            var dimNum1 = DimensionalNumberFactory.Length.Create(100, Prefix.Cento);

            var result = dimNum1.ChangePrefix(Prefix.One);
            var stringified = result.ToString();

            Assert.Equal("1 m", stringified);
        }

        [Fact]
        public void Cento_to_one_2()
        {
            var dimNum1 = DimensionalNumberFactory.Length.Create(123, Prefix.Cento);

            var result = dimNum1.ChangePrefix(Prefix.One);
            var stringified = result.ToString();

            Assert.Equal("1.23 m", stringified);
        }
        #endregion

        #region milli
        [Fact]
        public void One_to_milli()
        {
            var dimNum1 = DimensionalNumberFactory.Length.Create(1);

            var result = dimNum1.ChangePrefix(Prefix.Milli);
            var stringified = result.ToString();

            Assert.Equal("1000 mm", stringified);
        }

        [Fact]
        public void Cento_to_milli()
        {
            var dimNum1 = DimensionalNumberFactory.Length.Create(10, Prefix.Cento);

            var result = dimNum1.ChangePrefix(Prefix.Milli);
            var stringified = result.ToString();

            Assert.Equal("100 mm", stringified);
        }

        [Fact]
        public void Milli_to_cento()
        {
            var dimNum1 = DimensionalNumberFactory.Length.Create(10, Prefix.Milli);

            var result = dimNum1.ChangePrefix(Prefix.Cento);
            var stringified = result.ToString();

            Assert.Equal("1 cm", stringified);
        }

        [Fact]
        public void Milli_to_cento_2()
        {
            var dimNum1 = DimensionalNumberFactory.Length.Create(5, Prefix.Milli);

            var result = dimNum1.ChangePrefix(Prefix.Cento);
            var stringified = result.ToString();

            Assert.Equal("0.5 cm", stringified);
        }
        #endregion
    }
}