using NUnit.Framework;
using POC_DotNET_6;
using System.Threading.Tasks;

namespace TestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1()
        {
            var promise = new Promise<string>((resolve, reject) =>
            {
                resolve("Hello word");
            });

            var result = await promise.Execute();
            Assert.AreEqual("Hello word", result);
        }
    }
}