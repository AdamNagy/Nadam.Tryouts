using MyMessageQueue;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace UriTreeTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Valid_Contains()
        {
            var uriString = "https://gate.shop/hu/noi-polok-es-felsok?filter%5Bto%5D=1000";
            var uri = new Uri(uriString);

            var tree = new UriTree(uriString);

            Assert.AreEqual(uri.Host, tree.Domain);
            Assert.IsTrue(tree.Contains("https://gate.shop/hu/noi-polok-es-felsok?filter%5Bto%5D=1000"));
        }


        [Test]
        public void InValid_Contains()
        {
            var uriString = "https://ipon.hu/shop/csopo/notebook-szamitogep/6";

            var tree = new UriTree(uriString);
            Assert.IsFalse(tree.Contains("https://ipon.hu/shop/csoport/notebook-szamitogep/5"));
        }

        [Test]
        public void Add_Many_Uri()
        {
            var uris = File.ReadAllLines(@"C:\Users\Adam_Nagy1\Documents\test-urls-to-download.txt")
                .Where(p => !string.IsNullOrEmpty(p))
                .Where(p => p.ToLower().Contains("gate"))
                .Take(100)
                .ToList();

            var tree = new UriTree(uris[0]);
            foreach (var item in uris)
            {
                tree.AddUri(item);
            }

            foreach (var item in uris)
            {
                Assert.IsTrue(tree.Contains(item));
            }

            Assert.AreEqual(uris.Count, tree.Count);
        }

        [Test]
        public void Get_Many_Uri()
        {
            var uris = File.ReadAllLines(@"C:\Users\Adam_Nagy1\Documents\test-urls-to-download.txt")
                .Where(p => !string.IsNullOrEmpty(p))
                .Where(p => p.ToLower().Contains("gate"))
                .Take(100)
                .Select(p => p.Trim('/'))
                .ToList();

            var tree = new UriTree(uris[0]);
            foreach (var item in uris)
            {
                tree.AddUri(item);
            }

            var idx = 0;
            var treeUris = tree.GetUris().ToList();
            foreach (var item in uris)
            {
                ++idx;
                Assert.IsTrue(treeUris.Contains(item));
            }
        }

        [Test]
        public void Valid_Get()
        {
            var uriString = "https://gate.shop/hu/noi-polok-es-felsok?filter%5Bto%5D=1000";
            var uri = new Uri(uriString);

            var tree = new UriTree(uriString);

            var treeUris = tree.GetUris().ToList()[0];

            Assert.AreEqual(treeUris, uriString);
        }
    }
}