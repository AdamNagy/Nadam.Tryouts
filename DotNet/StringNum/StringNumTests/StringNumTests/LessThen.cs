using StringNumSet;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace StringNumTests
{
    public class LessThen
    {
        [Fact]
        public void OneLThenZero()
        {
            var result = "1".ToStringNum() < "0".ToStringNum();
            Assert.False(result);
        }

        [Fact]
        public void MOneLThenZero()
        {
            var result = "-1".ToStringNum() < "0".ToStringNum();
            Assert.True(result);
        }

        [Fact]
        public void ZeroLThenOne()
        {
            var result = "0".ToStringNum() < "1".ToStringNum();
            Assert.True(result);
        }

        [Fact]
        public void ZeroLThenZero()
        {
            var result = "0".ToStringNum() < "-1".ToStringNum();
            Assert.False(result);
        }

        [Fact]
        public void OneLThenPOne()
        {
            var result = "1".ToStringNum() < "1".ToStringNum();
            Assert.False(result);
        }

        [Fact]
        public void MOneLThenNOne()
        {
            var result = "-1".ToStringNum() < "1".ToStringNum();
            Assert.True(result);
        }

        [Fact]
        public void MOneLThenNOne2()
        {
            var result = "1".ToStringNum() < "-1".ToStringNum();
            Assert.False(result);
        }

        [Fact]
        public void MOneLThenMNOne()
        {
            var result = "-1".ToStringNum() < "-1".ToStringNum();
            Assert.False(result);
        }

        [Fact]
        public void Normal()
        {
            var result = "123".ToStringNum() < "32".ToStringNum();
            Assert.False(result);
        }

        [Fact]
        public void Normal2()
        {
            var result = "12".ToStringNum() < "321".ToStringNum();
            Assert.True(result);
        }

        [Fact]
        public void Normal3()
        {
            var result = "123".ToStringNum() < "123".ToStringNum();
            Assert.False(result);
        }

        [Fact]
        public void SameLength1()
        {
            var result = "123456".ToStringNum() < "123455".ToStringNum();
            Assert.False(result);
        }

        [Fact]
        public void SameLength2()
        {
            var result = "123455".ToStringNum() < "123456".ToStringNum();
            Assert.True(result);
        }

        [Fact]
        public void SameLength3()
        {
            var result = "20".ToStringNum() < "13".ToStringNum();
            Assert.False(result);
        }
    }
}
