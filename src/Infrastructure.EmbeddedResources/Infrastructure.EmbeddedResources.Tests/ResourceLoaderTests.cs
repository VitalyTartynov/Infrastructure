using System.Reflection;
using NUnit.Framework;

namespace Infrastructure.EmbeddedResources.Tests
{
    [TestFixture]
    internal class ResourceLoaderTests
    {
        [Test]
        public void GetResourceAsStreamTest()
        {
            var path = @"Samples\testfile.txt";
            var stream = ResourceLoader.GetEmbeddedResource(Assembly.GetExecutingAssembly(), path);

            Assert.That(stream, Is.Not.Null);
        }
    }
}