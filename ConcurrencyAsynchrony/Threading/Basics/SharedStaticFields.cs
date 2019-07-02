using System.Threading;

using static System.Console;

using static Core.ThreadHelper;

namespace ConcurrencyAsynchrony.Threading.Basics
{
    class SharedStaticFields
    {
        static bool _done;

        internal void SharedState()
        {
            // Static fields are shared between all threads
            // in the same application domain.
            Thread t = new Thread(Go);
            t.Name = "SSF";
            t.Start();
            Go();
        }

        void Go()
        {
            DisplayCurrentThreadInfo("Entering");
            if (!_done)
            {
                Thread.Sleep(0);
                // This test result in "Done" being 
                // printed once instead of twice.
                _done = true;
                WriteLine("ssf:Done:" + Thread.CurrentThread.Name);
            }
            DisplayCurrentThreadInfo("Exiting");
        }
    }
}
