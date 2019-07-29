using System.Threading;
using System.Threading.Tasks;

using static System.Console;

using static Core.ThreadHelper;

namespace StreamsIO
{
    class Program
    {
        /// <summary>
        /// Demonstrates usage of streams and IO.
        /// </summary>
        /// <param name="args">Program arguments</param>
        static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "MAIN";

            DisplayCurrentThreadInfo("Entering");

            // BACKING STORE STREAMS
            WriteLine("BACKING STORE STREAMS");
            WriteLine();
            //_ = BackingStoreDemoAsync();

            // DECORATOR STREAMS
            WriteLine();
            WriteLine();
            WriteLine("DECORATOR STREAMS");
            WriteLine();
            _ = DecoratorsDemoAsync();

            // STREAM ADAPTERS
            WriteLine();
            WriteLine();
            WriteLine("STREAM ADAPTERS");
            WriteLine();
            //AdaptersDemo();

            // Since tasks use pooled threads by default,
            // which are background threads. This means
            // that when the main thread ends, so do any
            // tasks that you create. In a Console app,
            // you must block the main thread after
            // starting a task (by waiting the task or
            // by calling `Console.ReadLine`).
            ReadLine();

            DisplayCurrentThreadInfo("Exiting");
        }

        /// <summary>
        /// Demonstrates usage of backing store streams.
        /// </summary>
        /// <returns>A task that is signaled upon completion or fault.</returns>
        static async Task BackingStoreDemoAsync()
        {
            await BackingStore.Demo.TestAsync();
        }
        /// <summary>
        /// Demonstrates usage of decorator streams.
        /// </summary>
        static async Task DecoratorsDemoAsync()
        {
            await Decorators.Demo.TestAsync();
        }

        /// <summary>
        /// Demonstrates usage of adapter streams.
        /// </summary>
        static void AdaptersDemo()
        {
            Adapters.Demo.Test();
        }
    }
}
