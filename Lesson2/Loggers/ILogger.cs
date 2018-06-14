namespace Lesson2.Loggers
{
    /// <summary>
    /// Интерфейс логгера
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Метод обычного сообщения
        /// </summary>
        /// <param name="message">Сообщение</param>
        void Print(string message);

        /// <summary>
        /// Метод обычного сообщения с аргументами
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="args">Аргументы</param>
        void Print(string message, params object[] args);
        
        /// <summary>
        /// Метод сообщения об ошибке
        /// </summary>
        /// <param name="message">Сообщение</param>
        void ErrorPrint(string message);
        
        /// <summary>
        /// Метод сообщение об ошибке с аргументами
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="args">Аргументы</param>
        void ErrorPrint(string message, params object[] args);
    }
}