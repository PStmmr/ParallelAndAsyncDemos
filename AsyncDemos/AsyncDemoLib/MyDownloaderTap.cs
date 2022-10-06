using System;
using System.Threading.Tasks;

namespace AsyncDemoLib
{
    public class MyDownloaderTap
    {
        public async Task<byte[]> DownloadOnePageAsync(Uri uri)
        {
            //synchronous method which returns byte[] --> is translated to async Task 
            Task<byte[]> task = new Task<byte[]>( () => new MyDownloaderEap().Download(uri)); 
            task.Start();
            return await task;
        }

    }
}