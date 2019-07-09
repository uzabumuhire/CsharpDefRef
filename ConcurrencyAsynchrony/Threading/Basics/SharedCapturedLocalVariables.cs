using System.Threading;

using static System.Console;

using static Core.ThreadHelper;

namespace ConcurrencyAsynchrony.Threading.Basics
{
    class SharedCapturedLocalVariables
    {
        /// <summary>
        /// Demonstrates how local variables are captured
        /// by a lambda expression and become a shared state.
        /// </summary>
        internal void SharedState()
        {
            // Local variables captured by a lambda expression
            // or anonymous delegate are converted by the compiler
            // into fields, and so can also be shared.
            bool done = false;

            ThreadStart action = () =>
            {
                DisplayCurrentThreadInfo("Entering");
                if (!done)
                {
                    Thread.Sleep(0);
                    // This test result in "Done" being 
                    // printed once instead of twice.
                    done = true;
                    WriteLine("sclv:Done:"+Thread.CurrentThread.Name);
                }
                DisplayCurrentThreadInfo("Exiting");
            };

            Thread t = new Thread(action);
            t.Name = "SCLV";
            t.Start();
            action();
        }
    }
}
