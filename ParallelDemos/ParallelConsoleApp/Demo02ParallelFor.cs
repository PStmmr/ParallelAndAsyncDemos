using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelConsoleApp
{
    class Demo02ParallelFor
    {
        static void Main()
        {
            Console.WriteLine("Parallel.For loop.");

            ParallelLoopResult loopResult = Parallel.For(1, 10000,
                                            i => // loop body - might be executed in parallel
                                            { 
                                                Console.WriteLine("Step {0}", i);
                                                //Thread.Sleep(new Random().Next(20, 100));
                                            });

            // Parallel.For returns when all iterations are executed - or exception occured; 
            // The loop iterations could be executed in parallel
            Console.WriteLine("Parallel.For completed: {0}", loopResult.IsCompleted);
            Console.WriteLine("Hit Enter.");
            Console.ReadLine();

            ForLoopThrowsException();
            Console.ReadLine();
        }

        static void ForLoopThrowsException()
        {
            try
            {
                Parallel.For(1, 10, i =>
                                {
                                    Console.WriteLine("Step {0}", i);
                                    if (i > 5) { throw new ApplicationException("bäh"); }
                                });
            }
            catch (AggregateException ae)
            {
                Console.WriteLine("InnerExceptions Count: {0}", ae.InnerExceptions.Count());
                ae.Handle((ex) =>
                {
                    if (ex is ApplicationException)
                    {
                        Console.WriteLine(ex.Message);
                        return true;
                    } 
                    return false;
                }); 

            }
        }
    }
}
