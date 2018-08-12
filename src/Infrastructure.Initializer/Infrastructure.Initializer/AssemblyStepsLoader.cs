using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Infrastructure.Initializer
{
    /// <summary>
    /// Загрузчик шагов инициализации и отката из сборки
    /// </summary>
    public class AssemblyStepsLoader : IStepsLoader
    {
        private readonly Assembly _assembly;

        /// <summary>
        /// Загрузка шагов инициализации и отката из сборки
        /// </summary>
        public AssemblyStepsLoader(Assembly assembly)
        {
            _assembly = assembly;
        }

        public ICollection<IInitializeStep> Load()
        {
            var types = _assembly
                .GetTypes()
                .Where(x => x.IsClass && typeof(IInitializeStep).IsAssignableFrom(x))
                .ToArray();

            return types
                .Select(x => Activator.CreateInstance(x) as IInitializeStep)
                .OrderBy(x => x.Priority)
                .ToArray();
        }
    }
}