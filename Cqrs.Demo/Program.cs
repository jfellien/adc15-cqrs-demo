using System;
using Fluent_CQRS;
using Fluent_CQRS.Extensions;

namespace Cqrs.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleLogger.Log("Lets start to do CQRS").AsInfo();
            
            ConsoleLogger.Log("Hit [ENTER] ... ").AsInfo();
            Console.ReadLine();
        }
    }
}
