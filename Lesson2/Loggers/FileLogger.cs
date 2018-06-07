using System.IO;

namespace Lesson2.Loggers
{
    //TODO: переписать на более производительный вид
    public class FileLogger : ILogger
    {
        private StreamWriter _mainLog;
        private StreamWriter _errorLog;

        public void Print(string message)
        {
            _mainLog = new StreamWriter("MainLog.log", true);
            _mainLog.WriteLine(message);
            _mainLog.Close();
        }

        public void Print(string message, params object[] args)
        {
            _mainLog = new StreamWriter("MainLog.log", true);
            _mainLog.WriteLine(message, args);
            _mainLog.Close();
        }

        public void ErrorPrint(string message)
        {
            _errorLog = new StreamWriter("ErrorLog.log", true);
            _errorLog.WriteLine(message);
            _errorLog.Close();
        }

        public void ErrorPrint(string message, params object[] args)
        {
            _errorLog = new StreamWriter("ErrorLog.log", true);
            _errorLog.WriteLine(message, args);
            _errorLog.Close();
        }
    }
}