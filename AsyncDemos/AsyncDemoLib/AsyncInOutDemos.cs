using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDemoLib
{
    public class AsyncInOutDemos
    {
        /// <summary>
        /// Synchronusly download the data for each given uri - one after the other.
        /// </summary>
        /// <param name="urlList"></param>
        /// <returns></returns>
        public static int SumPageSizes(IList<Uri> urlList)
        {
            int total = 0;
            try
            {
                WebClient webClient = new WebClient();
                foreach (Uri uri in urlList)
                {
                    byte[] data = webClient.DownloadData(uri);
                    total += data.Length;
                }
            }
            // No exception is expected.
            catch (Exception ex)
            {
                Console.WriteLine("Synchronous foreach has thrown an exception. THIS WAS NOT EXPECTED.\n{0}", ex);
            }

            return total;
        }

        /// <summary>
        /// Downloads the url data and calculates the length asyncrhonously.
        /// Here we use the new async / await key words - so we create a download Task for each url and await the result.
        /// </summary>
        /// <param name="urlList"></param>
        /// <returns></returns>
        public static async Task<int> SumPageSizesTaskAsync(List<Uri> urlList)
        {
            int total = 0;

            try
            {
                foreach (Uri uri in urlList)
                {
                    //starts a download task for each uri
                    byte[] data = await new WebClient().DownloadDataTaskAsync(uri);
                    Interlocked.Add(ref total, data.Length); //total += data.Length;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("async / await has thrown an exception. THIS WAS NOT EXPECTED.\n{0}", ex);
            }
            return total;
        }

        /// <summary>
        /// Downloads the url data and calculates the length using <see cref="Parallel"/> ForEach.
        /// </summary>
        /// <param name="urlList"></param>
        /// <returns></returns>
        public static int SumPageSizesParallelForEach(List<Uri> urlList)
        {
            int total = 0;
            try
            {
                Parallel.ForEach(urlList, uri => //body of a synchronous foreach() loop
                {
                    byte[] data = new WebClient().DownloadData(uri);
                    Interlocked.Add(ref total, data.Length); //total += data.Length;
                });
            }
            // No exception is expected in this example, but if one is still thrown from a task, 
            // it will be wrapped in AggregateException and propagated to the main thread. 
            catch (AggregateException e)
            {
                Console.WriteLine("Parallel.ForEach has thrown an exception. THIS WAS NOT EXPECTED.\n{0}", e);

            }

            return total;
        }

    }
}
