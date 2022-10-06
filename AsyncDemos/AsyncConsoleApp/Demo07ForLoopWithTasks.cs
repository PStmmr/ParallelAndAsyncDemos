using System;
using System.Threading.Tasks;

namespace AsyncConsoleApp
{
    class Demo07ForLoopWithTasks
    {
        static void Main()
        {
            const int upper = 10000;
            Console.WriteLine("Starting {0} operations asynchronously .... ", upper);
            
            for (int i = 0; i < upper; i++)
            {
                int taskId = i;
                Task t = DoWhatAsync(new Random().Next(1, 6));
                Task t2 = t.ContinueWith(
                    task => Console.WriteLine("Completed task {0}", taskId), 
                    TaskContinuationOptions.ExecuteSynchronously);
                
            }

            Console.WriteLine("Hit enter to stop.");
            Console.ReadLine();
        }

        private static Task DoWhatAsync(int i)
        {
            return Task.Delay(i*500);
        }
    }
}