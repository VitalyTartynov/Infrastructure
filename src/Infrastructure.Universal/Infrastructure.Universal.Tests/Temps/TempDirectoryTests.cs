using System.IO;
using Infrastructure.Universal.Temps;
using NUnit.Framework;

namespace Infrastructure.Universal.Tests.Temps
{
    /// <summary>
    /// Тесты класса для работы с временными директориями.
    /// </summary>
    [TestFixture]
    public class TempDirectoryTest
    {
        /// <summary>
        /// Комплексный тест работоспособности временных директорий.
        /// </summary>
        [Test]
        public void ComplexTest()
        {
            string directory;

            using (var tempDirectory = TempDirectory.Create())
            {
                directory = tempDirectory.Path;
                Assert.That(Directory.Exists(directory));
            }

            Assert.That(Directory.Exists(directory), Is.False);
        }
    }
}