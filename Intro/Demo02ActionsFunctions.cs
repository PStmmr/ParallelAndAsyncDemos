using System;
using System.Collections;
using System.Collections.Generic;

namespace Intro
{
    public class MyMath
    {
        public static int Add(int a, int b)
        {
            return a + b;
        }

        public static int Sub(int a, int b)
        {
            return a - b;
        }
    }

    public class Demo02ActionsFunctions
    {
        
        static void Main(string[] args)
        {
            Action<string> logInfo = MyLog.WriteInfo;
            logInfo.Invoke("Now we are using an Action<string>. Action has no return value.");

            //Functions in C#: first we declare return argument type, then input argument types.
            Func<int, int, int> f1 = MyMath.Add;
            Func<int, int, int> f2 = MyMath.Sub;

            Console.WriteLine("f(x) -> f1(a, f2(b,c)) = {0} ", f1(100, f2(10, 5)));

            Console.ReadLine();
        }
    }

}