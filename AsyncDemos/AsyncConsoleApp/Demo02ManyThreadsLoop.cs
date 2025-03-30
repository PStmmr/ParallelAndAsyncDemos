using System;
using System.Threading;

namespace AsyncConsoleApp
{
    class Demo02ManyThreadsLoop
    {
        // Synchronous BLOCKING method
        static void WaitForTwoSeconds()
        {
            Thread.Sleep(2000); //BLOCKS the executing thread for 2 seconds -> don't use in real world :-)
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