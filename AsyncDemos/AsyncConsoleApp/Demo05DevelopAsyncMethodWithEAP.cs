using System;
using System.Threading;
using AsyncDemoLib;

namespace AsyncConsoleApp
{
    class Demo05DevelopAsyncMethodWithEap
    {
        static void Main()
        {
            Console.WriteLine("Start download async");
            MyDownloaderEap downloader = new MyDownloaderEap();
            Uri uriToDownload = new Uri(@"http://tgw-group.com");

            downloader.DownloadCompleted += (sender, args) =>
                                                {
                                                    if (args.Error == null )
                                                    {
                                                        Console.WriteLine("Downloaded {0} bytes - index: {1}",
                                                            args.Result.Length, args.UserState);
                                                    } else
                                                    {
                                                        Console.WriteLine("Download failed. Eror: {0}", args.Error);
                                                    }
                                                };
            
            for (int i = 0; i < 1; i++)
            {
                downloader.DownloadAsync(uriToDownload, i);    
            }
            
            Console.WriteLine("Running on thread {0}. Hit enter to stop.", Thread.CurrentThread.ManagedThreadId);
            Console.ReadLine();
        }
        
    }
}