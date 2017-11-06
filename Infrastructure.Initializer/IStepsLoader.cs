using System.Collections.Generic;

namespace Infrastructure.Initializer
{
    /// <summary>
    /// Загрузчик шагов инициализации и отката
    /// </summary>
    public interface IStepsLoader
    {
        /// <summary>
        /// Загрузка шагов инициализации и отката
        /// </summary>
        /// <returns>Коллекция шагов инициализации и отката</returns>
        ICollection<IInitializeStep> Load();
    }
}