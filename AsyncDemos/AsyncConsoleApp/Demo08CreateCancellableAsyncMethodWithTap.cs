using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncConsoleApp
{
    class Demo08CreateCancellableAsyncMethodWithTap
    {
        
        static void Main()
        {
          

            SimpleDownloader downloader = new SimpleDownloader();
            List<string> pageAddresses = new List<string>()
                                             {
                                                 @"https://www.orf.at/",
                                                 @"https://www.avanade.com/en-us",
                                                 @"https://www.microsoft.com/",
                                                  @"https://www.apple.com/"
                                             };
            Console.WriteLine("Start downloading {0} pages ...", pageAddresses.Count);

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            IProgress<int> progressReport = new Progress<int>( OnProgressReported);
            Task<IList<string>> task = downloader.DownloadHtmlPagesAsync(
                                                    pageAddresses, 
                                                    cancellationTokenSource.Token,
                                                    progressReport);
            task.ContinueWith(t =>
                                  {
                                      if (t.IsCanceled) {
                                          Console.WriteLine("Operation cancelled by user!");
                                      } 
                                      else if (t.Exception == null)
                                      {
                                          Console.WriteLine("We can now process the downloaded HTML texts.");
                                          Console.WriteLine("We retrieved {0} HTML pages.",t.Result.Count);
                                      }
                                  }, 
                                  TaskContinuationOptions.ExecuteSynchronously); 


            Console.WriteLine("Waiting for results (we are not blocked :-)) ... Hit enter to cancel.");
            Console.ReadLine();

            if (!task.IsCompleted) //task is still running - so cancel it ...
            {
                cancellationTokenSource.Cancel();
                Console.WriteLine("Cancel requested ...");
                Console.ReadLine();
            }
        }

        private static void OnProgressReported(int progressStep)
        {
            Console.WriteLine("Progress: {0}", progressStep);
        }
    }

    internal class SimpleDownloader
    {
        public string DownloadString(string address)
        {
            WebClient client = new WebClient();
            //if proxy requires authentication ...
            IWebProxy theProxy = client.Proxy;
            if (theProxy != null)
            {
                theProxy.Credentials = CredentialCache.DefaultCredentials;
            }
            return client.DownloadString(address);
        }

        public Task<IList<string>> DownloadHtmlPagesAsync(IList<string> addressesToDownload,
                                                          CancellationToken cancel, 
                                                          IProgress<int> progress)
        {
            if (addressesToDownload == null) {throw new ArgumentNullException("addressesToDownload");}
            if (addressesToDownload.Count == 0) {throw new ArgumentException("No web addresses specififed.");}

            int progressFactor = (100/addressesToDownload.Count);
            Task<IList<string>> downloadAllTask = new Task<IList<string>>( 
                                                    // add "long running" synchronous logic 
                                                    new Func<IList<string>>( () =>
                                                    {
                                                        IList<string> result = new List<string>();
                                                        //another option: create a child task for each download
                                                        for(int i = 0; i < addressesToDownload.Count; i++) 
                                                        {
                                                            cancel.ThrowIfCancellationRequested(); //shall we cancel?

                                                            string aAddress = addressesToDownload[i];
                                                            result.Add(DownloadString(aAddress));
                                                            
                                                            if (progress != null)  //inform about progress
                                                            {
                                                                progress.Report( (i +1) * progressFactor);
                                                            }
                                                        }
                                                        return result;
                                                    }),  
                                                    cancel);
            downloadAllTask.Start();
            return downloadAllTask;
        }
    }
}