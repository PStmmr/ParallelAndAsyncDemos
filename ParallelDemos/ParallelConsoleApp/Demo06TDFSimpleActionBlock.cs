using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataFlowDemoLib;

namespace ParallelConsoleApp
{
    class Demo06TDFSimpleActionBlock
    {
        static void Main()
        {
            Console.WriteLine("Demo for a simple TDF ActionBlock<T>");
            Demo01SimpleActionBlock.SyncActionBlockSample1();
            Console.ReadLine();
        }
    }
}
