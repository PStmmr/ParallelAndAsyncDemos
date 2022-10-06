using System;
//using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Threading;

namespace AsyncConsoleApp
{
    class Demo04UseEap
    {
        public static byte[] DownloadData(Uri uri)
        {
            byte[] data = new WebClient().DownloadData(uri);
            return data;
        }

        static void Main()
        {

            Uri tgwUri = new Uri(@"http://www.tgw-group.com");
            Console.WriteLine("Downloading from {0} synchron using EAP", tgwUri);
            Stopwatch stopwatch = new Stopwatch(); 
            
            stopwatch.Start();
            byte[] data = DownloadData(tgwUri);
            stopwatch.Stop();

            Console.WriteLine("Downloaded {0} bytes in {1} ms.", data.Length, stopwatch.ElapsedMilliseconds);

            Uri sswUri = new Uri(@"http://ssw.jku.at");
            DownloadUsingEap(tgwUri);
            DownloadUsingEap(sswUri);


            Console.WriteLine("Hit enter to shutdown.");
            Console.ReadLine();
        }

        private static void DownloadUsingEap(Uri tgwUri)
        {
            Console.WriteLine("Start downloading data using EAP. My Thread ID: {0}", Thread.CurrentThread.ManagedThreadId);

            WebClient webClient = new WebClient();
            webClient.DownloadDataCompleted += (sender, args) =>
                                                   {
                                                       if (args.Error == null)
                                                       {
                                                           Console.WriteLine("Download {0} bytes from {1} completed.",
                                                                                args.Result.Length, args.UserState);
                                                       }
                                                       else
                                                       {
                                                           Console.WriteLine("Error: {0}", args.Error);
                                                       }
                                                       Console.WriteLine("Callback Thread ID: {0}", Thread.CurrentThread.ManagedThreadId);
                                                   };

            webClient.DownloadDataAsync(tgwUri, tgwUri); //start async operation - use uri as user state - reused in completed event handler

        }
    }
}