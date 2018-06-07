namespace Lesson2.Loggers
{
    public interface ILogger
    {
        void Print(string message);
        void Print(string message, params object[] args);
        void ErrorPrint(string message);
        void ErrorPrint(string message, params object[] args);
    }
}