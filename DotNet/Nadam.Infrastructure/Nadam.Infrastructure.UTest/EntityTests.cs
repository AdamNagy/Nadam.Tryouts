using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nadam.Infrastructure.UTest.Gallery;

namespace Nadam.Infrastructure.UTest
{
    [TestClass]
    public class EntityTests
    {
        [TestMethod]
        public void CreatingObject()
        {
            var galleryModel = new GalleryModel();
            Assert.IsNotNull(galleryModel);
        }

        [TestMethod]
        public void UpdatePropertyFromNull()
        {
            var galleryModel = new GalleryModel();

            var newValues = new List<int>() { 2, 4, 6, 8, 10 };
            galleryModel.Update("Urls", newValues);

            Assert.AreEqual(newValues, galleryModel.Urls);
        }

        [TestMethod]
        public void UpdatePropertyFromValidState()
        {
            var galleryModel = new GalleryModel();
            galleryModel.Urls = new List<int>(){1,3,5,7,9};

            var newValues = new List<int>() {2, 4, 6, 8, 10};
            galleryModel.Update("Urls", newValues);

            Assert.AreEqual(newValues, galleryModel.Urls);
        }

        [TestMethod]
        public void UpdatePropertyToNull()
        {
            var galleryModel = new GalleryModel();
            galleryModel.Urls = new List<int>() { 1, 3, 5, 7, 9 };

            var newValues = new List<int>() { 2, 4, 6, 8, 10 };
            galleryModel.Update("Urls", null);

            Assert.IsNull(galleryModel.Urls);
        }

        [TestMethod]
        public void UpdateMultipleProperties()
        {
            var model = new TestClassModel()
            {
                intProp = 10,
                str = "Hello world",
                datetime = new DateTime(2019, 1, 3)
            };

            var expectecValues = new TestClassModel()
            {
                intProp = 20,
                str = "qwe",
                datetime = new DateTime(2018, 3, 1)
            };

            model.Update("intProp", expectecValues.intProp);
            model.Update("str", expectecValues.str);
            model.Update("datetime", expectecValues.datetime);

            Assert.AreEqual(expectecValues.intProp, model.intProp);
            Assert.AreEqual(expectecValues.str, model.str);
            Assert.AreEqual(expectecValues.datetime, model.datetime);
        }
    }
}
