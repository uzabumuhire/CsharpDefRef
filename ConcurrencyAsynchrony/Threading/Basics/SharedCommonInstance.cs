using System.Threading;

using static System.Console;

using static Core.ThreadHelper;

namespace ConcurrencyAsynchrony.Threading.Basics
{
    class SharedCommonInstance
    {
        
        bool _done;

        internal void SharedState()
        {
            // Threads share data if they have a common reference
            // to the same object instance.

            // Because both threads call Go() on the same `CommoInstance`
            // reference `sci`, they share the `_done` field.
            SharedCommonInstance sci = new SharedCommonInstance(); // create a common instance
            Thread t = new Thread(sci.Go);
            t.Name = "SCI";
            t.Start();
            sci.Go();
        }

        void Go()
        {
            DisplayCurrentThreadInfo("Entering");
            // Note that this is an instance method.
            if (!_done)
            {
                Thread.Sleep(0);
                // This test result in "Done" being 
                // printed once instead of twice.
                _done = true;
                WriteLine("sci:Done:"+Thread.CurrentThread.Name);
            }
            DisplayCurrentThreadInfo("Exiting");
        }
    }
}
