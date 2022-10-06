using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ParallelConsoleApp
{

    class Demo04PLinqAndFiles
    {
        static void Main()
        {
            string path = @"C:\Temp";
            Console.WriteLine("Start ...");
            IterateFiles(path);
            Console.ReadLine();
        }


        public static void IterateFiles(string path)
        {
            var fileNames = from dir in Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories)
                            select dir;

            Stopwatch sw = Stopwatch.StartNew();
            int count = 0;
            var fileContents = from file in fileNames.AsParallel() // Use AsOrdered to preserve source ordering 
                               let extension = Path.GetExtension(file)
                               where extension == ".txt" || extension == ".htm"
                               let Text = File.ReadAllText(file)
                               select new { Text, FileName = file }; //Or ReadAllBytes, ReadAllLines, etc. 
            try
            {
                foreach (var item in fileContents)
                {
                    Console.WriteLine(Path.GetFileName(item.FileName) + ":" + item.Text.Length);
                    count++;
                }
            }
            catch (AggregateException ae)
            {
                ae.Handle((ex) =>
                {
                    if (ex is UnauthorizedAccessException)
                    {
                        Console.WriteLine(ex.Message);
                        return true;
                    }
                    return false;
                });
            }
            sw.Stop();
            Console.WriteLine("Processed {0} files using in {1} milliseconds", count, sw.ElapsedMilliseconds);
        }

     
    }


}
