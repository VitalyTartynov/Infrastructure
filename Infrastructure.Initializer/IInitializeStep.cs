namespace Infrastructure.Initializer
{
    /// <summary>
    /// Шаг инициализации и отката
    /// </summary>
    public interface IInitializeStep
    {
        /// <summary>
        /// Приоритет шага в последовательности инициализации
        /// </summary>
        int Priority { get; }
        
        /// <summary>
        /// Описание
        /// </summary>
        string Summary { get; }

        /// <summary>
        /// Инициализация
        /// </summary>
        void Setup();

        /// <summary>
        /// Откат
        /// </summary>
        void Teardown();
    }
}