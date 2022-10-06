using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace DataFlowDemoLib
{
    public class Demo02BufferBlock_ProducerConsumer
    {
        int itemCount;
        BufferBlock<int> bufferBlock = new BufferBlock<int>();
        bool isRunning = false;

        private int GenerateItem()
        {
            Thread.Sleep(100); //simulate long running operation ...
            itemCount++;
            return itemCount;
        }

        /// <summary>
        /// Produce items and post them to the block;
        /// </summary>
        private void Producer()
        {
            while (isRunning)
            {
                int item = GenerateItem();
                bufferBlock.Post(item);
            }
            //bufferBlock.Complete();

        }
        
        /// <summary>
        /// Wait for items to be received from the block and process them.
        /// </summary>
        private async Task Consumer()
        {
            while (isRunning || bufferBlock.Count > 0) //classical check - better to use CancellationToken in block.Receive ...!
            {
                //synchronous receive: bufferBlock.Receive();
                int item = await bufferBlock.ReceiveAsync(); //asynchronous receive ... in a Task
                Process(item);
            }
        }

        private void Process(int item)
        {
            Console.WriteLine("Processing item {0}" , item);
            Thread.Sleep(100); // simulate long running operation
        }

        public void RunDemo()
        {
            itemCount = 0;
            isRunning = true;
            Task producer = Task.Factory.StartNew(Producer);
            Task consumer = Consumer();

            Console.WriteLine("Started producer and consumer tasks ...");
            Console.ReadLine();
            Console.WriteLine("Stopping ...");
            
            isRunning = false;
            Task.WaitAll(producer, consumer); //blocking wait for completion
        }

       
    }
}
