using System;
using System.IO;

namespace Infrastructure.Universal.Temps
{
    /// <summary>
    /// Создание временного файла в файловой системе и его автоматическое удаление при выходе из блока кода.
    /// </summary>
    public class TempFile : IDisposable
    {
        /// <summary>
        /// Полный путь к временному файлу.
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// Создаёт временный файл, который расположен в указанной директории, имеет указанное расширение 
        /// и заполнен указанными данными.
        /// </summary>
        /// <param name="extension">Требуемое расширение временного файла.</param>
        /// <param name="content">Требуемое содержимое временного файла.</param>
        /// <param name="parentDir">Директория, в которой должен быть расположен файл. Если значение этого параметра не указано,
        /// файл будет создан в директории <c>%TEMP%</c>.</param>
        /// <returns>Созданный и заполненный временный файл.</returns>
        public static TempFile Create(string extension, byte[] content, string parentDir = "")
        {
            var tempFile = new TempFile(extension, parentDir);

            try
            {
                using (var fileStream = File.Create(tempFile.Path))
                {
                    fileStream.Write(content, 0, content.Length);
                }

                return tempFile;
            }
            catch
            {
                tempFile.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Создаёт временный файл, который расположен в указанной директории, имеет указанное расширение 
        /// и заполнен указанными данными.
        /// </summary>
        /// <param name="extension">Требуемое расширение временного файла.</param>
        /// <param name="content">Поток с требуемым содержимым временного файла.</param>
        /// <param name="parentDir">Директория, в которой должен быть расположен файл. Если значение этого параметра не указано,
        /// файл будет создан в директории <c>%TEMP%</c>.</param>
        /// <returns>Созданный и заполненный временный файл.</returns>
        public static TempFile Create(string extension, Stream content, string parentDir = "")
        {
            var tempFile = new TempFile(extension, parentDir);

            try
            {
                using (var fileStream = File.Create(tempFile.Path))
                {
                    content.Seek(0, SeekOrigin.Begin);
                    content.CopyTo(fileStream);
                    fileStream.Flush(flushToDisk: true);
                }

                return tempFile;
            }
            catch
            {
                tempFile.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Создаёт пустой временный файл, который расположен в указанной директории и имеет указанное расширение.
        /// </summary>
        /// <param name="extension">Требуемое расширение временного файла.</param>
        /// <param name="parentDir">Директория, в которой должен быть расположен файл. Если значение этого параметра не указано,
        /// файл будет создан в директории <c>%TEMP%</c>.</param>
        /// <returns>Созданный временный файл.</returns>
        public static TempFile Create(string extension, string parentDir = "")
        {
            var tempFile = new TempFile(extension, parentDir);

            try
            {
                using (File.Create(tempFile.Path))
                {
                    // Цель этого блока кода - просто создать пустой файл по указанному пути.
                    // Поэтому мы сразу закрываем поток с данными файла, ничего в него не записывая.
                }

                return tempFile;
            }
            catch
            {
                tempFile.Dispose();
                throw;
            }
        }

        private TempFile(string extension, string parentDir)
        {
            extension = FixExtension(extension);

            Path = string.IsNullOrEmpty(parentDir)
                ? GetTempFile(extension)
                : GetTempFileInDirectory(parentDir, extension);
        }

        public void Dispose()
        {
            if (File.Exists(Path))
            {
                File.Delete(Path);
                Path = string.Empty;
            }
        }

        private string GetTempFileInDirectory(string parentDir, string extension)
        {
            var fileName = System.IO.Path.GetRandomFileName();
            var ext = System.IO.Path.GetExtension(fileName);
            var path = System.IO.Path.Combine(parentDir, fileName);
            path = path.Replace(ext, extension);

            return path;
        }

        private static string FixExtension(string extension)
        {
            return extension.StartsWith(".") ? extension : $".{extension}";
        }

        /// <summary>
        /// Путь к временному файлу с заданным расширением
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        private static string GetTempFile(string extension)
        {
            var path = System.IO.Path.GetTempFileName();
            var ext = System.IO.Path.GetExtension(path);
            path = path.Replace(ext, extension);

            return path;
        }
    }
}