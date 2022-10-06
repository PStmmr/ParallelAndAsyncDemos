using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncConsoleApp
{
    class Demo06DownloadUseTask
    {
        static void Main()
        {
            Uri downloadUri = new Uri(@"http://localhost/UIConceptsWebApp");
            Console.WriteLine("Downloading from {0} asynchronously using TAP", downloadUri);

            //synchronous code:
            //byte[] result = new WebClient().DownloadData(tgwUri);
            //Console.WriteLine("Downloaded {0} bytes. (SYNC)", result.Length);
            
            Task<byte[]> downloadTask = new WebClient().DownloadDataTaskAsync(downloadUri);
            downloadTask.ContinueWith(task =>
                 Console.WriteLine("Downloaded {0} bytes. (ASYNC)", task.Result.Length)
                 );

            Console.WriteLine("Hit enter to shutdown.");
            Console.ReadLine();
        }


        //static Task<byte[]> GrabImageAsync(string pathToImage, CancellationToken cancellationToken) 
        //{
        //    TaskCompletionSource<byte[]> taskCompletionSource = new TaskCompletionSource<byte[]>();
            

            
        
        //}
        
    }
}