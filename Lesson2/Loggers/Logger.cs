using System;

namespace Lesson2.Loggers
{
    public static class Logger
    {
        private delegate void PrintFunction(string message);

        private delegate void PrintArgsFunction(string message, params object[] args);

        private static PrintFunction _printFunction;
        private static PrintArgsFunction _printArgsFunction;

        private static PrintFunction _errorPrintFunction;
        private static PrintArgsFunction _errorPrintArgsFunction;

        /// <summary>
        /// Метод инициализации логгера
        /// </summary>
        /// <param name="logger"></param>
        [Obsolete("Init is obsolate, please use AddLogger instead")]
        public static void Init(ILogger logger)
        {
            AddLogger(logger);
        }

        /// <summary>
        /// Метод добавления логгера
        /// </summary>
        /// <param name="logger"></param>
        public static void AddLogger(ILogger logger)
        {
            _printFunction += logger.Print;
            _printArgsFunction += logger.Print;

            _errorPrintFunction += logger.ErrorPrint;
            _errorPrintArgsFunction += logger.ErrorPrint;
        }

        /// <summary>
        /// Метод удаление логгера
        /// </summary>
        /// <param name="logger"></param>
        public static void RemoveLogger(ILogger logger)
        {
            _printFunction -= logger.Print;
            _printArgsFunction -= logger.Print;

            _errorPrintFunction -= logger.ErrorPrint;
            _errorPrintArgsFunction -= logger.ErrorPrint;
        }

        /// <summary>
        /// Метод обычного сообщения
        /// </summary>
        /// <param name="message">Сообщение</param>
        public static void Print(string message)
        {
            _printFunction?.Invoke(message);
        }

        /// <summary>
        /// Метод обычного сообщения с аргументами
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="args">Аргументы</param>
        public static void Print(string message, params object[] args)
        {
            _printArgsFunction?.Invoke(message, args);
        }

        /// <summary>
        /// Метод сообщения об ошибке
        /// </summary>
        /// <param name="message">Сообщение</param>
        public static void Error(string message)
        {
            _errorPrintFunction?.Invoke(message);
        }

        /// <summary>
        /// Метод сообщение об ошибке с аргументами
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="args">Аргументы</param>
        public static void Error(string message, params object[] args)
        {
            _errorPrintArgsFunction?.Invoke(message, args);
        }
    }
}