using System.Collections.Generic;

namespace Infrastructure.Initializer
{
    /// <summary>
    /// Базовая реализация инициализатора
    /// </summary>
    public abstract class Initializer
    {
        /// <summary>
        /// Базовая реализация инициализатора
        /// </summary>
        /// <param name="initializers">Коллекция шагов инициализации</param>
        protected Initializer(ICollection<IInitializeStep> initializers)
        {
            Initializers = initializers;
        }

        /// <summary>
        /// Шаги инициализации
        /// </summary>
        protected ICollection<IInitializeStep> Initializers { get; }

        /// <summary>
        /// Выполнение инициализации всех шагов
        /// </summary>
        public abstract void Setup();

        /// <summary>
        /// Выполнение отката всех шагов
        /// </summary>
        public abstract void Teardown();
    }
}