using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelConsoleApp
{
    class Demo03PLinq
    {

        static void Main()
        {
            IEnumerable<int> source = Enumerable.Range(10, 1000);

            // Opt-in to PLINQ with AsParallel() 
            ParallelQuery<int> evenNumsQuery = from num in source.AsParallel()
                           where Compute(num) > 0
                           select num;

            Console.WriteLine("Found {0} even numbers.", evenNumsQuery.Count());


            var parallelQuery = from num in source.AsParallel()
                                where num % 10 == 0
                                select num;

            // Process result sequence in parallel
            parallelQuery.ForAll((e) => DoSomething(e));

            // Or use foreach to merge results first. 
            foreach (var n in parallelQuery)
            {
                Console.WriteLine(n);
            }

            // You can also use ToArray, ToList, etc 
            // as with LINQ to Objects. 
            var parallelQuery2 = (from num in source.AsParallel()
                                  where num % 10 == 0
                                  select num).ToArray();

            // Method syntax is also supported 
            var parallelQuery3 = source.AsParallel().Where(n => n % 10 == 0).Select(n => n);


            Console.ReadLine();
        }

        static int Compute(int anInt) // should be long running ... 
        {
            return anInt % 2;
        }

        static void DoSomething(int element)
        {
            Console.WriteLine("DoSomething: {0}" , element);
          
        }
    }

}
