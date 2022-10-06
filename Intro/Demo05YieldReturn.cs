using System;
using System.Collections.Generic;

namespace Intro
{
    class Demo05YieldReturn
    {
        static void Main(string[] args)
        {

            IEnumerable<int> myFibs = Fibs(7);

            foreach (int aFib in myFibs)
            {
                Console.WriteLine(aFib);
            }

            Console.ReadLine();
        }

        private static IEnumerable<int> Fibs(int count)
        {
            int fib1 = 0;
            int fib2 = 1;
            for (int i = 0; i < count; i++)
            {
                int nextFib = fib1 + fib2; // Fibonacci calculation
                fib1 = fib2;
                fib2 = nextFib;
                yield return nextFib;
            }
            
        }
    }
}