using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace AsyncConsoleApp
{
    class Demo03IAsyncResultPattern
    {

        public static int WaitSeconds(int seconds)
        {
            Thread.Sleep(seconds * 1000); //for demo only - blocking current thread!
            int threadId = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine("Done waiting {0} seconds on Thread {1}", seconds, threadId);
            return threadId;
        }

        static void Main()
        {
            Console.WriteLine("APM sample. first we call method synchronously");
            int id = WaitSeconds(2);
            Console.WriteLine("Sync call ended on thread {0}. Now we call it async.", id);

            IAsyncResult asyncResult = BeginWaitSeconds(2, //in argument - wait 2 seconds
                                                    ar => //async callback, where we fetch the result.
                                                        {
                                                            Console.WriteLine("Did operation complete? {0}", ar.IsCompleted);
                                                            int threadId = EndWaitSeconds(ar);
                                                            Console.WriteLine("Returned id: {0}. My id is: {1}", 
                                                                threadId, Thread.CurrentThread.ManagedThreadId);
                                                        }, 
                                                    "userState");

            Console.WriteLine("Async OP started, my id is: {0}. Async operation already completed: {1}", 
                              Thread.CurrentThread.ManagedThreadId, asyncResult.IsCompleted);
            Console.WriteLine("Hit enter to stop");
            Console.ReadLine();
        }

       

        public static IAsyncResult BeginWaitSeconds(int seconds, AsyncCallback callback, object userState)
        {
            Func<int, int> asyncFunctionDelegate = WaitSeconds;
            return asyncFunctionDelegate.BeginInvoke(seconds, callback, userState);
        }

        public static int EndWaitSeconds(IAsyncResult arHandle)
        {
            Func<int, int> asyncFunctionDelegate;
            try
            {
                //getting called delegate function - these casts could go wrong!
                AsyncResult asyncResult = (AsyncResult)arHandle;
                asyncFunctionDelegate = (Func<int, int>)asyncResult.AsyncDelegate;
            }
            catch (InvalidCastException ex)
            {
                throw new InvalidOperationException("Wrong asyncResult handlse!", ex);
            }

            return asyncFunctionDelegate.EndInvoke(arHandle);
        }

    }
}