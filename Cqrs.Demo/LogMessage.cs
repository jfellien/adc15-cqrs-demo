using System;

namespace Cqrs.Demo
{
    public class LogMessage
    {
        readonly string _logmessage;
        readonly ConsoleColor _defaultForegroundColor;
        bool _withTimeStamp;

        public LogMessage(string logmessage)
        {
            _logmessage = logmessage;
            _defaultForegroundColor = System.Console.ForegroundColor;
        }

        public LogMessage WithTimeStamp()
        {
            _withTimeStamp = true;

            return this;
        }

        public void AsInfo()
        {
            Console.ForegroundColor = ConsoleColor.White;
            WriteMessage();
        }

        public void AsWarning()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            WriteMessage();
        }

        public void AsError()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            WriteMessage();
        }

        public void AsDebug()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            WriteMessage();
        }

        private void WriteMessage()
        {
            if (_withTimeStamp)
            {
                Console.WriteLine("[{0} {1}] - {2}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), _logmessage);
            }
            else
            {
                Console.WriteLine(_logmessage);
            }
            Console.ForegroundColor = _defaultForegroundColor;
        }
    }
}