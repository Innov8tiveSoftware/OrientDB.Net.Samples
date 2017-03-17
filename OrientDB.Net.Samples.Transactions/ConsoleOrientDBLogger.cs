using OrientDB.Net.Core.Abstractions;
using System;

namespace OrientDB.Net.Samples.Transactions
{
    class ConsoleOrientDBLogger : IOrientDBLogger
    {
        public void Debug(string message)
        {
            Console.WriteLine(message);
        }

        public void Error(string message)
        {
            Console.WriteLine(message);
        }

        public void Fatal(string message)
        {
            Console.WriteLine(message);
        }

        public void Information(string message)
        {
            Console.WriteLine(message);
        }

        public void Verbose(string message)
        {
            Console.WriteLine(message);
        }

        public void Warning(string message)
        {
            Console.WriteLine(message);
        }
    }
}
