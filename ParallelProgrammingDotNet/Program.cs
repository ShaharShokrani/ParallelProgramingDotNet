using System;
using ParallelLinq;

namespace ParallelProgrammingDotNet
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            CancellationAndExceptions.Do();
        }
    }
}
