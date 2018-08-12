using System.IO;
using Infrastructure.Universal.Temps;
using NUnit.Framework;

namespace Infrastructure.Universal.Tests.Temps
{
    /// <summary>
    /// Тесты класса для работы со временными файлами.
    /// </summary>
    [TestFixture]
    public class TempFileTests
    {
        /// <summary>
        /// Комплексный тест работоспособности временных файлов.
        /// </summary>
        [Test]
        public void ComplexTest()
        {
            var extension = ".dwg";
            string path;

            using (var tempFile = TempFile.Create(extension: extension, content: new byte[0]))
            {
                path = tempFile.Path;
                Assert.That(File.Exists(path));
                Assert.That(Path.GetExtension(path), Is.EqualTo(extension));
            }

            Assert.That(File.Exists(path), Is.False);
        }

        /// <summary>
        /// Тест создания временного файла в конкретной директории
        /// </summary>
        [Test]
        public void CreateTempFileInConcreteDirectoryTest()
        {
            var extension = ".dwg";
            string path;

            using (var tempDir = TempDirectory.Create())
            {
                using (var tempFile = TempFile.Create(extension: extension, content: new byte[0], parentDir: tempDir.Path))
                {
                    path = tempFile.Path;
                    Assert.That(File.Exists(path));
                    Assert.That(Path.GetExtension(path), Is.EqualTo(extension));
                    Assert.That(Path.GetDirectoryName(path), Is.EqualTo(tempDir.Path));
                }
            }

            Assert.That(File.Exists(path), Is.False);
        }

        /// <summary>
        /// Проверка того, что корректно обрабатывается расширение файла
        /// </summary>
        [Test]
        public void FixExtensionDotTest()
        {
            var extension = "dwg";
            var expectedExtension = $".{extension}";
            string path;

            using (var tempFile = TempFile.Create(extension: extension, content: new byte[0]))
            {
                path = tempFile.Path;
                Assert.That(File.Exists(path));
                Assert.That(Path.GetExtension(path), Is.EqualTo(expectedExtension));
            }

            Assert.That(File.Exists(path), Is.False);
        }
    }
}