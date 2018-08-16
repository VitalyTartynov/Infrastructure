using System;
using System.IO;
using System.Reflection;

namespace Infrastructure.Universal.Paths
{
    public static class PathHelper
    {
        /// <summary>
        /// Путь к текущей исполняемой сборке.
        /// </summary>
        /// <returns>Путь к сборке.</returns>
        public static string AssemblyDirectory()
        {
            return AssemblyDirectory(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Путь к директории, в которой расположена сборка.
        /// </summary>
        /// <param name="assembly">Сборка, путь к которой ищется.</param>
        /// <returns>Путь к сборке.</returns>
        public static string AssemblyDirectory(Assembly assembly)
        {
            return Path.GetDirectoryName(AssemblyPath(assembly));
        }

        /// <summary>
        /// Полный путь к текущей исполняемой сборке.
        /// </summary>
        /// <returns>Полный путь к сборке.</returns>
        public static string AssemblyPath()
        {
            return AssemblyPath(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Полный путь к указанной сборке.
        /// </summary>
        /// <param name="assembly">Сборка, путь к которой ищется.</param>
        /// <returns>Полный путь к сборке.</returns>
        public static string AssemblyPath(Assembly assembly)
        {
            var codeBase = assembly.CodeBase;
            var uri = new UriBuilder(codeBase);

            return Uri.UnescapeDataString(uri.Path);
        }
    }
}