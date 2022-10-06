using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataFlowDemoLib;

namespace ParallelConsoleApp
{
    class Demo07ProducerConsumerV2TDF
    {
        static void Main()
        {
            Console.WriteLine("Producer / Consumer using BufferBlock<T>.");
            Demo02BufferBlock_ProducerConsumer demo = new Demo02BufferBlock_ProducerConsumer();
            demo.RunDemo();
            Console.WriteLine("Done - hit enter to close");
            Console.ReadLine();
        }
    }
}
