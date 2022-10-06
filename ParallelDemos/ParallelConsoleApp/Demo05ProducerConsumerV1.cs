using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace ParallelConsoleApp
{
    class Demo05ProducerConsumerV1
    {
        static void Main()
        {
            BlockingCollection<int> blockingCollection = new BlockingCollection<int>();
            Console.WriteLine("One producer task adds items, another task consumes them using {0}.", blockingCollection.GetType());

            Task producer = new TaskFactory().StartNew(
                        async () =>
                        {
                            for (int i = 1; i < 10; i++)
                            {
                                blockingCollection.Add(i);
                                await Task.Delay(1000); //wait a seond - cooperative wait
                            }
                            Console.WriteLine("Producing stopped");
                            blockingCollection.CompleteAdding(); //signal waiting consumers we are done
                        },
                        TaskCreationOptions.LongRunning
                );

            Task consumer = new TaskFactory().StartNew( //or Task.Run(...)
                        () =>
                        {
                            int localSum = 0;
                            //waits until next element is available
                            foreach (int nextItem in blockingCollection.GetConsumingEnumerable()) 
                            {
                                localSum += nextItem;
                                Console.WriteLine("{0} /  {1}", nextItem, localSum);
                            }
                        },
                        TaskCreationOptions.LongRunning
                    );

            Console.WriteLine("All started - hit enter to stop ...");
            Console.ReadLine();
            Console.WriteLine("Producer coompleted: {0}", producer.IsCompleted);
            Console.WriteLine("Consumer coompleted: {0}", consumer.IsCompleted);
            Console.ReadLine();
        }
    }
}
