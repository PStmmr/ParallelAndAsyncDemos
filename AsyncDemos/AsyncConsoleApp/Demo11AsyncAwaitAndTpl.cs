using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AsyncDemoLib;

namespace AsyncConsoleApp
{
    public class Demo11AsyncAwaitAndTpl
    {
        static void Main(string[] args)
        {
            List<Uri> urlList = new List<Uri>
                                    {
                                        new Uri(@"https://www.avanade.com/en-us"),
                                        new Uri(@"http://www.ssw.jku.at"),
                                        new Uri(@"http://www.microsoft.com"),
                                        new Uri(@"http://www.orf.at")
                                    };

            Demo01Synchron.DownloadPageSizes(urlList);
            Console.ReadLine();

            DownloadPageSizesUsingAsyncTasksAsync(urlList).Wait();
            
            DownloadPageSizesUsingParallel(urlList);
            Console.ReadLine();

           
        }

        public static async Task DownloadPageSizesUsingAsyncTasksAsync(List<Uri> urlList)
        {
           
            Console.WriteLine("Downloading data from {0} web pages using await and async.", urlList.Count);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int size = await AsyncInOutDemos.SumPageSizesTaskAsync(urlList);

            stopwatch.Stop();
            Console.WriteLine("Downloaded {0} bytes in {1} ms.", size, stopwatch.ElapsedMilliseconds);
        }

        public static void DownloadPageSizesUsingParallel(List<Uri> urlList)
        {
           
            Console.WriteLine("Downloading data from {0} web pages using Parallel.ForEach .", urlList.Count);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int size = AsyncInOutDemos.SumPageSizesParallelForEach(urlList);

            stopwatch.Stop();
            Console.WriteLine("Downloaded {0} bytes in {1} ms.", size, stopwatch.ElapsedMilliseconds);
        }
    }
}
