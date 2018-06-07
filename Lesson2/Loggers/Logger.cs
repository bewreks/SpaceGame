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

        public static void Init(ILogger logger)
        {
            AddLogger(logger);
        }

        public static void AddLogger(ILogger logger)
        {
            _printFunction += logger.Print;
            _printArgsFunction += logger.Print;

            _errorPrintFunction += logger.ErrorPrint;
            _errorPrintArgsFunction += logger.ErrorPrint;
        }

        public static void RemoveLogger(ILogger logger)
        {
            _printFunction -= logger.Print;
            _printArgsFunction -= logger.Print;

            _errorPrintFunction -= logger.ErrorPrint;
            _errorPrintArgsFunction -= logger.ErrorPrint;
        }

        public static void Print(string message)
        {
            _printFunction?.Invoke(message);
        }

        public static void Print(string message, params object[] args)
        {
            _printArgsFunction?.Invoke(message, args);
        }

        public static void Error(string message)
        {
            _errorPrintFunction?.Invoke(message);
        }

        public static void Error(string message, params object[] args)
        {
            _errorPrintArgsFunction?.Invoke(message, args);
        }
    }
}