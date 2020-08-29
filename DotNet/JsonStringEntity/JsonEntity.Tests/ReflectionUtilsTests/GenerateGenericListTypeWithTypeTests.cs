using System;
using System.Collections.Generic;
using DataEntity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestData;

namespace ReflectionUtilsTests
{
    [TestClass]
    public class GenerateGenericListTypeWithTypeTests
    {
        [TestMethod]
        public void With_Int()
        {
            var result = ReflectionUtils.GenerateGenericListTypeWithType(typeof(int));

            var intList = new List<int>();
            var expected = intList.GetType();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void With_String()
        {
            var result = ReflectionUtils.GenerateGenericListTypeWithType(typeof(string));

            var intList = new List<string>();
            var expected = intList.GetType();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void With_Object()
        {
            var result = ReflectionUtils.GenerateGenericListTypeWithType(typeof(Object));

            var intList = new List<Object>();
            var expected = intList.GetType();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void With_CustomObject()
        {
            var result = ReflectionUtils.GenerateGenericListTypeWithType(typeof(TestJsonModel));

            var intList = new List<TestJsonModel>();
            var expected = intList.GetType();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void With_IntList()
        {
            var result = ReflectionUtils.GenerateGenericListTypeWithType(typeof(List<int>));

            var intList = new List<List<int>>();
            var expected = intList.GetType();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void With_StringList()
        {
            var result = ReflectionUtils.GenerateGenericListTypeWithType(typeof(List<string>));

            var intList = new List<List<string>>();
            var expected = intList.GetType();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void With_CustomObjectList()
        {
            var result = ReflectionUtils.GenerateGenericListTypeWithType(typeof(List<TestJsonModel>));

            var intList = new List<List<TestJsonModel>>();
            var expected = intList.GetType();

            Assert.AreEqual(expected, result);
        }
    }
}
