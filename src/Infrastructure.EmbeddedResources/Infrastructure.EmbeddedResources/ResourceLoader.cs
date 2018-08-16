using System;
using System.IO;
using System.Reflection;

namespace Infrastructure.EmbeddedResources
{
    /// <summary>
    /// Загрузчик ресурсов из сборки.
    /// </summary>
    public static class ResourceLoader
    {
        /// <summary>
        /// Получение ресурса из текущей исполняемой сборки.
        /// </summary>
        /// <param name="embeddedFilePath">Путь от корня сборки до требуемого файла - например,
        /// <c>Json\Resources\SomeDocument.json</c>. В качестве разделителя используется
        /// точка или обратный слэш.</param>
        /// <returns>Поток с данными ресурса.</returns>
        public static MemoryStream GetEmbeddedResource(string embeddedFilePath)
        {
            return GetEmbeddedResource(Assembly.GetExecutingAssembly(), embeddedFilePath);
        }

        /// <summary>
        /// Получение ресурса из указанной сборки.
        /// </summary>
        /// <param name="assembly">Сборка, в которой находится ресурс.</param>
        /// <param name="embeddedFilePath">Путь от корня сборки до требуемого файла - например,
        /// <c>Json\Resources\SomeDocument.json</c>. В качестве разделителя используется
        /// точка или обратный слэш.</param>
        /// <returns>Поток с данными ресурса.</returns>
        public static MemoryStream GetEmbeddedResource(Assembly assembly, string embeddedFilePath)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            if (string.IsNullOrWhiteSpace(embeddedFilePath))
            {
                throw new ArgumentException("Path to embedded file is empty string.");
            }

            var assemblyName = assembly.GetName().Name;
            var pathToResource = embeddedFilePath.Replace("\\", ".");
            var resourceName = $"{assemblyName}.{pathToResource}";

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new FileNotFoundException($"Assembly '{assemblyName}' doesn't contains embedded file at path '{embeddedFilePath}'.");
                }

                var memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);

                return memoryStream;
            }
        }

        /// <summary>
        /// Создание файла на диске из ресурса текущей исполняемой сборки.
        /// </summary>
        /// <param name="embeddedFileName">Имя файла в ресурсе.</param>
        /// <param name="fileName">Путь от корня сборки до требуемого файла - например,
        /// <c>Json\Resources\SomeDocument.json</c>. В качестве разделителя используется
        /// точка или обратный слэш.</param>
        public static void ExtractFileFromEmbeddedResource(string embeddedFileName, string fileName)
        {
            ExtractFileFromEmbeddedResource(Assembly.GetExecutingAssembly(), embeddedFileName, fileName);
        }

        /// <summary>
        /// Создание файла на диске из ресурса.
        /// </summary>
        /// <param name="resourcesAssembly">Сборка, содержащая ресурс.</param>
        /// <param name="embeddedFileName">Имя файла в ресурсе.</param>
        /// <param name="fileName">Путь от корня сборки до требуемого файла - например,
        /// <c>Json\Resources\SomeDocument.json</c>. В качестве разделителя используется
        /// точка или обратный слэш.</param>
        public static void ExtractFileFromEmbeddedResource(Assembly resourcesAssembly, string embeddedFileName, string fileName)
        {
            using (var stream = GetEmbeddedResource(resourcesAssembly, embeddedFileName))
            {
                using (var fileStream = File.Create(fileName))
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.CopyTo(fileStream);
                }
            }
        }

        /// <summary>
        /// Считывает содержимое указанного встроенного ресурса и возвращает его в виде текстовой строки.
        /// </summary>
        /// <param name="embeddedFileName">Путь от корня сборки до требуемого файла - например,
        /// <c>Json\Resources\SomeDocument.json</c>. В качестве разделителя используется
        /// точка или обратный слэш.</param>
        /// <returns>Строка с содержимым требуемого ресурса в виде текста.</returns>
        public static string GetTextFromEmbeddedResource(string embeddedFileName)
        {
            return GetTextFromEmbeddedResource(Assembly.GetExecutingAssembly(), embeddedFileName);
        }

        /// <summary>
        /// Считывает содержимое указанного встроенного ресурса и возвращает его в виде текстовой строки.
        /// </summary>
        /// <param name="assembly">Сборка, в которой содержится ресурс.</param>
        /// <param name="embeddedFileName">Путь от корня сборки до требуемого файла - например,
        /// <c>Json\Resources\SomeDocument.json</c>. В качестве разделителя используется
        /// точка или обратный слэш.</param>
        /// <returns>Строка с содержимым требуемого ресурса в виде текста.</returns>
        public static string GetTextFromEmbeddedResource(Assembly assembly, string embeddedFileName)
        {
            using (var memoryStream = GetEmbeddedResource(assembly, embeddedFileName))
            {
                memoryStream.Seek(0, SeekOrigin.Begin);

                using (var reader = new StreamReader(memoryStream))
                {
                    var fileContents = reader.ReadToEnd();
                    return fileContents;
                }
            }
        }
    }
}