using System;
using System.Collections.Generic;
using System.Threading;

namespace ParallelConsoleApp
{
    public class Demo01MyParallelForLoop
    {
        /// <summary>
        /// Represents a for loop which executes the given loop body <see cref="Action"/> in parallel 
        /// based on available processors.
        /// </summary>
        /// <param name="inclusiveLowerBound"></param>
        /// <param name="exclusiveUpperBound"></param>
        /// <param name="body">one iteration step - the loop body</param>
        public static void MyParallelFor(int inclusiveLowerBound, int exclusiveUpperBound, Action<int> body)
        {
            // Determine the number of iterations to be processed, the number of
            // cores to use, and the approximate number of iterations to process
            // in each thread.
            int size = exclusiveUpperBound - inclusiveLowerBound;
            int numProcs = Environment.ProcessorCount;
            int range = size / numProcs;
            // Use a thread for each partition. Create them all,
            // start them all, wait on them all.
            List<Thread> threads = new List<Thread>(numProcs);
            for (int p = 0; p < numProcs; p++)
            {
                int start = p * range + inclusiveLowerBound;
                int end = (p == numProcs - 1) ? exclusiveUpperBound : start + range;
                threads.Add(new Thread(() =>
                {
                    for (int i = start; i < end; i++)
                    {
                        body(i);
                    }
                }));
            }

            //start and wait ... 
            foreach (Thread thread in threads)
            {
                thread.Start();
            }
            foreach (Thread thread in threads)
            {
                thread.Join();
            }
        }

        static void Main()
        {
            Console.WriteLine("MyParallel FOR loop.");

            MyParallelFor(1, 1000,
                i =>
                {
                    Console.WriteLine("Step {0}", i);
                    Thread.Sleep(20);
                });

            Console.ReadLine();
        }

    }
}