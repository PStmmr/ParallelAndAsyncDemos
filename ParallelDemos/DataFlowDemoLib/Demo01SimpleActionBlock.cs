using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace DataFlowDemoLib
{
    public class Demo01SimpleActionBlock
    {
        public static void SyncActionBlockSample1()
        {
            Console.WriteLine("Start");
            //one synchronous action delegate is used to process the incomming / posted datum (aka message)
            ActionBlock<string> block = new ActionBlock<string>(
                aString => Console.WriteLine("Sample1 Processing {0}", aString));

            for (int i = 0; i < 20; i++)
            {
                block.Post("Item_" + i);
            }

        }

        public static void SyncActionBlockSample2()
        {

            ActionBlock<string> block = new ActionBlock<string>(aString => Console.WriteLine("Sample2 Processing {0}", aString),
                new ExecutionDataflowBlockOptions() { MaxDegreeOfParallelism = 5 });

            for (int i = 0; i < 20; i++)
            {
                block.Post("Item_" + i);
            }

        }

        #region async action block sample
        public static void AsyncActionBlockSample1()
        {
            ExecutionDataflowBlockOptions dfOptions = new ExecutionDataflowBlockOptions()
                                                          {
                                                              MaxDegreeOfParallelism = 4, 
                                                              //BoundedCapacity = 5,
                                                              MaxMessagesPerTask = 5
                                                          };
            //an async Task is created for each incomming / posted datum (aka message)
            ActionBlock<string> block = new ActionBlock<string>(new Func<string, Task>(ProcessStringAsync), 
                                        dfOptions);

            

            for (int i = 0; i < 50; i++)
            {
                string data = "Item_" + i;
                block.Post(data);
                //bool x = await block.SendAsync(data);
                //Console.WriteLine("SendAsync{0} completed with value: {1}", data, x);
                
            }

            Console.WriteLine("Start Thread id: {0}", Thread.CurrentThread.ManagedThreadId);
            ProcessStringAsync("Foo").ContinueWith(
                completedTask => Console.WriteLine("Continue Thread id: {0}", Thread.CurrentThread.ManagedThreadId));


        }

        private static Task ProcessStringAsync(string s)
        {
            Task t = new TaskFactory().StartNew(() =>
                                  {
                                      Console.WriteLine("{0} @ {1}", 
                                          s, 
                                          Thread.CurrentThread.ManagedThreadId);
                                      Thread.Sleep(60);
                                  });
            return t;
        }
        #endregion
    }
}
