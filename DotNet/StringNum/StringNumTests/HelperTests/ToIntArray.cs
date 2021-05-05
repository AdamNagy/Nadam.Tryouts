using StringNumSet;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace StringNumTests.HelperTests
{
    public class ToIntArray
    {
        [Fact]
        public void Test1()
        {
            var result = "123".ToIntArray();

            Assert.Equal(new int[] { 1, 2, 3 }, result);
        }
    }
}
