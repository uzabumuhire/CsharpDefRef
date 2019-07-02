using System.Threading;

using static System.Console;

using static Core.ThreadHelper;

namespace ConcurrencyAsynchrony.Threading.Basics
{
    class SharedStateThreadSafe
    {
        static bool _done;
        static readonly object _locker = new object();

        internal void SharedState()
        {
            // Static fields are shared between all threads
            // in the same application domain.
            Thread t = new Thread(Go);
            t.Name = "SSTS";
            t.Start();
            Go();
        }

        void Go()
        {
            DisplayCurrentThreadInfo("Entering");
            // C# provides the `lock` statement  for obtaining
            // an exclusive lock while reading and writing to
            // the shared field.

            // When two threads simultaneously contend a lock
            // (which can be upon any reference-type objec,
            // in this case, _locker), one thread waits, or
            // blocks, until the lock becomes available.
            // It ensures only one thread can enter its code
            // block at a time, and "Done" will be printed
            // just once. Code that's protected in such
            // manner - from indeterminacy in a multithreaded
            // context - is called *thread-safe*.
            lock (_locker)
            {
                if (!_done)
                {
                    //Thread.Sleep(0);
                    // This test result in "Done" being 
                    // printed once instead of twice.
                    WriteLine("ssts:Done:" + Thread.CurrentThread.Name);
                    _done = true;
                }
            }
            DisplayCurrentThreadInfo("Exiting");
        }
    }
}
