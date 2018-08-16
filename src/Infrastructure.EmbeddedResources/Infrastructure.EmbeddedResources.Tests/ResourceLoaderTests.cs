using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;

namespace Infrastructure.EmbeddedResources.Tests
{
    [TestFixture]
    internal class ResourceLoaderTests
    {
        private MemoryStream _stream;

        [SetUp]
        public void SetUp()
        {
            _stream = null;
        }

        [TearDown]
        public void TearDown()
        {
            _stream?.Dispose();
        }

        [TestCase(@"Samples.testfile.txt")]
        [TestCase(@"Samples\testfile.txt")]
        public void GetResourceStreamFromAssemblyTest(string embeddedFilePath)
        {
            _stream = ResourceLoader.GetEmbeddedResource(Assembly.GetExecutingAssembly(), embeddedFilePath);

            Assert.That(_stream, Is.Not.Null);
            Assert.That(_stream.Length, Is.EqualTo(8));
        }

        [Test]
        public void ThrowExceptionOnNullAssemblyTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _stream = ResourceLoader.GetEmbeddedResource(null, string.Empty);
            });
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("     ")]
        public void ThrowExceptionOnIncorrectResourcePathTest(string embeddedFilePath)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                _stream = ResourceLoader.GetEmbeddedResource(Assembly.GetExecutingAssembly(), embeddedFilePath);
            });
        }

        [Test]
        public void ThrowExceptionOnMissingResourceTest()
        {
            Assert.Throws<FileNotFoundException>(() =>
            {
                _stream = ResourceLoader.GetEmbeddedResource(Assembly.GetExecutingAssembly(), "MissingEmbeddedResourcePath");
            });
        }

        [Test]
        public void ExtractTextFromResourceTest()
        {
            var text = ResourceLoader.GetTextFromEmbeddedResource(Assembly.GetExecutingAssembly(),
                @"Samples.testfile.txt");

            Assert.That(text, Is.Not.Null);
            Assert.That(text, Is.EqualTo("12345"));
        }

        [Test]
        public void ExtractFileFromResourceTest()
        {
            var tempFilePath = Path.GetTempFileName();
            try
            {
                Assert.That(File.ReadAllText(tempFilePath), Is.EqualTo(string.Empty));

                ResourceLoader.ExtractFileFromEmbeddedResource(Assembly.GetExecutingAssembly(), @"Samples.testfile.txt", tempFilePath);

                Assert.That(File.Exists(tempFilePath), Is.True);
                var content = File.ReadAllText(tempFilePath);
                Assert.That(content, Is.EqualTo("12345"));
            }
            finally
            {
                if (File.Exists(tempFilePath))
                {
                    File.Delete(tempFilePath);
                }
            }
        }
    }
}