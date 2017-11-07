using System.IO;
using System.Reflection;

namespace Infrastructure.EmbeddedResources
{
    /// <summary>
    /// Загрузчик ресурсов из сборки
    /// </summary>
    public static class ResourceLoader
    {
        /// <summary>
        /// Получение ресурса из текущей исполняемой сборки
        /// </summary>
        /// <param name="internalPath">Внутренний путь до файла</param>
        /// <returns>
        /// возвращает поток</returns>
        public static MemoryStream GetEmbeddedResource(string internalPath)
        {
            return GetEmbeddedResource(Assembly.GetExecutingAssembly(), internalPath);
        }

        /// <summary>
        /// Получение ресурса из конкретной сборки
        /// </summary>
        /// <param name="assembly">Сборка, в которой есть ресурс</param>
        /// <param name="internalPath">Внутренний путь до файла</param>
        /// <returns>Поток</returns>
        public static MemoryStream GetEmbeddedResource(Assembly assembly, string internalPath)
        {
            var resourceName = string.Format("{0}.{1}", assembly.GetName().Name, internalPath.Replace(@"\", "."));
            var stream = assembly.GetManifestResourceStream(resourceName);
            var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);

            return memoryStream;
        }
    }
}