using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //DataFlowDemoLib.Demo01SimpleActionBlock.SyncActionBlockSample1();
            //DataFlowDemoLib.Demo01SimpleActionBlock.SyncActionBlockSample2();
            DataFlowDemoLib.Demo01SimpleActionBlock.AsyncActionBlockSample1();

            Console.WriteLine("Started");
            Console.ReadLine();
        }
    }
}
