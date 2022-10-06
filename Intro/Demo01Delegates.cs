using System;

namespace Intro
{
    public delegate void DoWhat(string argument);

    public class Demo01Delegates
    {
        static void Main(string[] args)
        {
            DelegatesSample();
            Console.ReadLine();
        }

        public static void DelegatesSample()
        {
            //declare some delegate variables
            DoWhat logInfo = MyLog.WriteInfo;
            DoWhat log1 = MyLog.WriteError;
            DoWhat log2 = Console.WriteLine;

            DoWhat logs = log1 + log2;

            //use delegate variables
            logInfo("Step 1: declare and assign delegates.");
            logInfo.Invoke("Step 2: use delegate variables and invoke them.");

            log1.Invoke("variables can be null!");

            log2.Invoke("Done!");

            logs("This is an invocation list");

            Console.ReadLine();
        }

       
    }
}