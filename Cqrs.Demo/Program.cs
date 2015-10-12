using System;
using Fluent_CQRS;
using Fluent_CQRS.Extensions;

namespace Cqrs.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleLogger.Log("Lets start to do CQRS").AsHappyMessage();
            
            

            ConsoleLogger.Log("Hit [ENTER] ... ").AsWarning();
            Console.ReadLine();
        }
    }
}
