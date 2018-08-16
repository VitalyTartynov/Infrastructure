using System;
using System.IO;
using System.Reflection;
using Infrastructure.Universal.Paths;
using NUnit.Framework;

namespace Infrastructure.Universal.Tests.Paths
{
    [TestFixture]
    public class PathHelperTests
    {
        private Assembly _assembly;
        private string _expectedPath;
        private string _expectedDirectory;

        [SetUp]
        public void SetUp()
        {
            _assembly = Assembly.GetExecutingAssembly();
            _expectedPath = Uri.UnescapeDataString(new UriBuilder(_assembly.CodeBase).Path);
            _expectedDirectory = Path.GetDirectoryName(_expectedPath);
        }

        [TearDown]
        public void TearDown()
        {
            _assembly = null;
            _expectedPath = null;
            _expectedDirectory = null;
        }

        [Test]
        public void AssemblyPathTest()
        {
            var path = PathHelper.AssemblyPath(_assembly);

            Assert.That(path, Is.Not.Null);
            Assert.That(path, Is.EqualTo(_expectedPath));
        }

        [Test]
        public void AssemblyFolderTest()
        {
            var directory = PathHelper.AssemblyDirectory();

            Assert.That(directory, Is.Not.Null);
            Assert.That(directory, Is.EqualTo(_expectedDirectory));
        }
    }
}