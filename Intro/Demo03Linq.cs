using System;
using System.Collections.Generic;
using System.Linq;

namespace Intro
{
    public static class LinqDemo
    {
        /// <summary>
        /// Extension method to iterate through a string collection.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="action"></param>
        public static void Iterate(this IEnumerable<string> collection, Action<string> action)
        {
            foreach (string anItem in collection)
            {
                action(anItem);
            }
            //return collection;
        }
    }

    class Demo03Linq
    {
        static void Main(string[] args)
        {
            List<string> names = new List<string> { "Jack", "Jill", "Lago" };

            var q = from n in names
                    where n.StartsWith("J", StringComparison.Ordinal)
                    select n;

           
            q.Iterate(aName => Console.WriteLine(aName)); //q.Iterate(Console.WriteLine);

            Console.ReadLine();
        }
    }
}
