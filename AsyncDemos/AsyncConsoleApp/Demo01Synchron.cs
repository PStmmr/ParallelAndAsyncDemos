using System;
using System.Collections.Generic;
using System.Diagnostics;
using AsyncDemoLib;

namespace AsyncConsoleApp
{
    public class Demo01Synchron
    {
        static void Main(string[] args)
        {
            List<Uri> urlList = new List<Uri>
                                    {
                                        new Uri(@"http://www.tgw-group.com"),
                                        new Uri(@"http://www.ssw.jku.at"),
                                        new Uri(@"http://www.microsoft.com"),
                                        new Uri(@"http://www.orf.at")
                                    };
            DownloadPageSizes(urlList);
            Console.ReadLine();
        }

        public static void DownloadPageSizes(List<Uri> urlList)
        {

            Console.WriteLine("Downloading data from {0} web pages synchron.", urlList.Count);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int size = AsyncInOutDemos.SumPageSizes(urlList);

            stopwatch.Stop();
            Console.WriteLine("Downloaded {0} bytes in {1} ms.", size, stopwatch.ElapsedMilliseconds);
        }
    }
}