using System;
using System.Threading.Tasks;
using AsyncDemoLib;

namespace AsyncConsoleApp
{
    class Demo10CreateAsyncMethodWithTapAndAsync
    {
        static void Main()
        {
            Uri uri = new Uri(@"http://www.microsoft.com");
            StartDemo1Async(uri).Wait();
            Console.WriteLine("hit enter ...");
            Console.ReadLine();
        }

        static async Task StartDemo1Async(Uri uri)
        {
            MyDownloaderTap myDownloader = new MyDownloaderTap();
            Task<byte[]> downloadTask = myDownloader.DownloadOnePageAsync(uri);
            Console.WriteLine("so .. we started async opeation");
            byte[] result = await downloadTask;
            Console.WriteLine("Downloaded {0}", result.Length);
        }

        

    }
}