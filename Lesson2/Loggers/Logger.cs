namespace Lesson2.Loggers
{
    public static class Logger
    {
        private delegate void PrintFunction(string message);

        private delegate void PrintArgsFunction(string message, params object[] args);

        private static PrintFunction _printFunction;
        private static PrintArgsFunction _printArgsFunction;

        public static void Init(ILogger logger)
        {
            AddLogger(logger);
        }

        public static void AddLogger(ILogger logger)
        {
            _printFunction += logger.Print;
            _printArgsFunction += logger.Print;
        }

        public static void RemoveLogger(ILogger logger)
        {
            _printFunction -= logger.Print;
            _printArgsFunction -= logger.Print;
        }

        public static void Print(string message)
        {
            _printFunction?.Invoke(message);
        }

        public static void Print(string message, params object[] args)
        {
            _printArgsFunction?.Invoke(message, args);
        }
    }
}