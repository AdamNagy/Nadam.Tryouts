using MyMessageQueue;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace UriTreeTests
{
    public class Tests
    {
        [Test]
        public void Contains_Single()
        {
            var uriString = "https://gate.shop/hu/noi-polok-es-felsok";
            var uri = new Uri(uriString);

            var tree = new UriTree(uriString);

            Assert.AreEqual(uri.Host, tree.Domain);
            Assert.IsTrue(tree.Contains("https://gate.shop/hu/noi-polok-es-felsok"));
        }

        [Test]
        public void Contains_Single_With_QUery()
        {
            var uriString = "https://gate.shop/hu/noi-polok-es-felsok?filter%5Bto%5D=1000";
            var uri = new Uri(uriString);

            var tree = new UriTree(uriString);

            Assert.AreEqual(uri.Host, tree.Domain);
            Assert.IsTrue(tree.Contains("https://gate.shop/hu/noi-polok-es-felsok?filter%5Bto%5D=1000"));
        }

        [Test]
        public void Not_Contains()
        {
            var uriString = "https://ipon.hu/shop/csopo/notebook-szamitogep/6";

            var tree = new UriTree(uriString);
            Assert.IsFalse(tree.Contains("https://ipon.hu/shop/csoport/notebook-szamitogep/5"));
        }

        [Test]
        public void Contains()
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
        }

        [Test]
        public void GetUris_Many()
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

            var treeUris = tree.GetUris().ToList();
            foreach (var item in uris)
            {
                Assert.IsTrue(treeUris.Contains(item));
            }
        }

        [Test]
        public void GetUris_Single()
        {
            var uriString = "https://gate.shop/hu/noi-polok-es-felsok";

            var tree = new UriTree(uriString);

            var treeUris = tree.GetUris().ToList().Last();

            Assert.AreEqual(treeUris, uriString);
        }

        [Test]
        public void Count_One()
        {
            var uriString = "https://gate.shop";

            var tree = new UriTree(uriString);

            Assert.AreEqual(1, tree.Count);
        }

        [Test]
        public void Count_Two()
        {
            var uriString = "https://gate.shop/stuffs";

            var tree = new UriTree(uriString);

            Assert.AreEqual(2, tree.Count);
        }

        [Test]
        public void Count_Three()
        {
            var uriString = "https://gate.shop/stuffs/child";

            var tree = new UriTree(uriString);

            Assert.AreEqual(3, tree.Count);
        }

        [Test]
        public void Count_Three_With_Duplicated()
        {
            var uriString = "https://gate.shop/stuffs";
            var tree = new UriTree(uriString);

            tree.AddUri("https://gate.shop/stuffs/child");


            Assert.AreEqual(3, tree.Count);
        }

        [Test]
        public void Count_Four()
        {
            var uriString = "https://gate.shop/stuffs";
            var tree = new UriTree(uriString);

            tree.AddUri("https://gate.shop/stuffs2/child");

            Assert.AreEqual(4, tree.Count);
        }
    }
}