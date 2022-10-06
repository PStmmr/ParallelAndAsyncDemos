using System;
using System.Collections.Generic;

namespace Intro
{
    class Demo04ClosureWithAnonymousMethod
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Example 1: introducing a closure");
            int x = 1; //value type - not reference type
            Action action = () =>
                                {
                                    x++; //closure - accessing x from outer context 
                                    Console.WriteLine(x);
                                };

            action();              // Outputs '2'

            //Console.WriteLine(x);  // what value has x? 1 or 2?
            
            
            Example2();

            Console.ReadLine();
        }

        /// <summary>
        /// Shows modified closure + how to fix it.
        /// </summary>
        static void Example2()
        {
            Console.WriteLine("Example 2: closure + modified values");

            List<Action> actions = new List<Action>();
            for (int counter = 0; counter < 10; counter++)
            {
                actions.Add(() =>
                    {
                        Console.WriteLine(counter); 
                    }); // what value???? access to modified closure
            }

            // Then execute them
            foreach (Action anAction in actions)
            {
                anAction();
            }
        }

        
    }
}