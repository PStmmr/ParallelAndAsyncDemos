using System;
using System.Threading;

namespace AsyncConsoleApp
{
    class Demo02ManyThreadsLoop
    {
        // Synchronous BLOCKING method
        static void WaitForTwoSeconds()
        {
            Thread.Sleep(2000);
            Console.WriteLine("Thread {0} done.", Thread.CurrentThread.ManagedThreadId);
        }

        static void Main()
        {
            const int upperBoundary = 10000;
            Console.WriteLine("starting {0} threads.", upperBoundary);
            for (int i = 0; i < upperBoundary; i++)
            {
                new Thread(WaitForTwoSeconds).Start();
            }
            Console.ReadLine();
        }
    }

    


}