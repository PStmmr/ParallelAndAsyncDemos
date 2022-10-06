using System;
using System.Net;
using System.Threading.Tasks;

namespace AsyncConsoleApp
{
    class Demo09DownloadUseTapWithAwait
    {
        static void Main()
        {
            Uri tgwUri = new Uri(@"http://www.tgw-group.com");
            Console.WriteLine("Downloading from {0} asynchronously using TAP with await", tgwUri);


            DownloadAndPrintAsync(tgwUri).Wait();

            Console.WriteLine("Hit enter to shutdown.");
            Console.ReadLine();
        }

        static async Task DownloadAndPrintAsync(Uri uri)
        {
            //synchron code:
            //byte[] result = new WebClient().DownloadData(uri);
            //Console.WriteLine("Downloaded {0} bytes. (SYNC)", result.Length);

            try
            {
                byte[] result = await new WebClient().DownloadDataTaskAsync(uri);
                Console.WriteLine("Downloaded {0} bytes. (ASYNC)", result.Length);
                int i = 3;
                Console.WriteLine("my number {0}", i);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error downloading: {0}", ex);
            }

        }




        static async Task ExceptionSampleAsync()
        {
            string html;
            try
            {
                html = await (new WebClient().DownloadStringTaskAsync(new Uri("http://no.x/")));
            }
            catch (Exception ex)
            {
                html = "Error! " + ex;
            }
            Console.WriteLine(html);
        }

        static void DownloadBadImplAsync(Uri uri)
        {
            Task t = DownloadAndPrintAsync(uri);
        }

        
    }
}