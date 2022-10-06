using System;
using System.ComponentModel;
using System.Net;
using System.Runtime.Remoting.Messaging;

namespace AsyncDemoLib
{
    public class DownloadCompletedEventArgs : AsyncCompletedEventArgs
    {
        public DownloadCompletedEventArgs(byte[] resultData, object userState)
            : this(resultData, null, false, userState)
        {

        }

        public DownloadCompletedEventArgs(byte[] resultData, Exception error, bool cancelled, object userState)
            : base(error, cancelled, userState)
        {
            Result = resultData;
        }

        public byte[] Result { get; private set; }
    }

    public class MyDownloaderEap
    {
        /// <summary>
        /// Occurs when the async method <see cref="DownloadAsync"/> completed.
        /// </summary>
        public event EventHandler<DownloadCompletedEventArgs> DownloadCompleted;

        /// <summary>
        /// Downloads bytes from the given Uri synchronously.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public byte[] Download(Uri uri)
        {
            return new WebClient().DownloadData(uri);
        }

        /// <summary>
        /// Downloads bytes from the given Uri asynchronously using EAP - see also <seealso cref="DownloadCompleted"/> event.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="userState"></param>
        public void DownloadAsync(Uri uri, object userState)
        {
            BackgroundWorker backgroundWorker = new BackgroundWorker()
                                                    {
                                                        WorkerReportsProgress = false,
                                                        WorkerSupportsCancellation = false
                                                    };

            //here we call the long running synchronous method - the actual logic we want to execute asynchronously
            backgroundWorker.DoWork += (sender, args) =>
                                        {
                                            byte[] data = Download(args.Argument as Uri);
                                            args.Result = data;
                                        };

            //BackgroundWorker also uses EAP - we simply forward completed event
            backgroundWorker.RunWorkerCompleted += (sender, args) => OnDownloadCompleted(
                                                                        new DownloadCompletedEventArgs(args.Result as byte[],
                                                                                    args.Error,
                                                                                    args.Cancelled,
                                                                                    userState));
            // start 
            backgroundWorker.RunWorkerAsync(uri);

            #region done using IAsyncResult + delegate
            //we use the APM to implement our EAP - we could also use ThreadPool.QueueUserWorkItem or Delegate.BeginInvoke .... 
            //Func<Uri, byte[]> downloadDelegte = this.Download;
            //downloadDelegte.BeginInvoke(uri,
            //    ar =>
            //    {
            //        DownloadCompletedEventArgs completedEventArgs;
            //        try
            //        {
            //            Func<Uri, byte[]> calledMethod = ((AsyncResult)ar).AsyncDelegate as Func<Uri, byte[]>;
            //            byte[] data = calledMethod.EndInvoke(ar);
            //            completedEventArgs = new DownloadCompletedEventArgs(data, ar.AsyncState);
            //        }
            //        catch (Exception ex)
            //        {
            //            completedEventArgs = new DownloadCompletedEventArgs(null, ex, false, ar.AsyncState);
            //        }

            //        OnDownloadCompleted(completedEventArgs);

            //    },
            //    userState);
            #endregion
        }

        protected void OnDownloadCompleted(DownloadCompletedEventArgs e)
        {
            EventHandler<DownloadCompletedEventArgs> handler = DownloadCompleted;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}