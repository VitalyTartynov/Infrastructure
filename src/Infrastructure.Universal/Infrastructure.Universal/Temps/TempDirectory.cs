using System;
using System.IO;

namespace Infrastructure.Universal.Temps
{
    /// <summary>
    /// Создание временной директории и ее автоматическое удаление при выходе из блока кода.
    /// </summary>
    public class TempDirectory : IDisposable
    {
        /// <summary>
        /// Полный путь к временной директории.
        /// </summary>
        public string Path { get; private set; }

        public static TempDirectory Create()
        {
            return new TempDirectory();
        }

        private TempDirectory()
        {
            var tempDir = GetTempDirectoryPath();
            Directory.CreateDirectory(tempDir);

            Path = tempDir;
        }

        public void Dispose()
        {
            if (Directory.Exists(Path))
            {
                Directory.Delete(Path, recursive: true);
                Path = string.Empty;
            }
        }

        private string GetTempDirectoryPath()
        {
            var path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), System.IO.Path.GetRandomFileName());

            return path;
        }
    }
}