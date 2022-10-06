using System;

namespace Intro
{
    public class MyLog
    {

        public static void WriteInfo(string message)
        {
            Console.WriteLine("INFO: {0}", message);
        }

        public static void WriteError(string errorMessage)
        {
            Console.WriteLine("ERROR: {0}", errorMessage);
        }
    }
}