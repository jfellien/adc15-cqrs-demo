using System;

namespace Cqrs.Demo
{
    public class ConsoleLogger
    {
        public static LogMessage Log(string message)
        {
            return new LogMessage(message);
        }

        public static void EmptyRow()
        {
            Console.WriteLine();
        }
    }
}