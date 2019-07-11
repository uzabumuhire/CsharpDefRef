using System.Threading;
using System.Threading.Tasks;

using static System.Console;

using static Core.ThreadHelper;

namespace ConcurrencyAsynchrony
{
    /// <summary>
    /// Demonstrates usage of concurrency and asynchrony.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Tests usage of concurrency and ansychrony.
        /// </summary>
        /// <param name="args">Program arguments.</param>
        static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "MAIN";

            DisplayCurrentThreadInfo("Entering");

            // THREADING
            WriteLine("THREADING");
            WriteLine();
            //ThreadingDemo();

            // TASKS
            WriteLine();
            WriteLine();
            WriteLine("TASKS");
            WriteLine();
            //TasksDemo();

            // ASYNCHRONOUS PROGRAMMING
            WriteLine();
            WriteLine();
            WriteLine("ASYNCHRONOUS PROGRAMMING");
            WriteLine();
            _ = AsynchronousProgrammingDemoAsync();

            // Since tasks use pooled threads by default,
            // which are backgroung  threads. This means
            // that when the main thread ends, so do any
            // tasks that you create. In a Console app,
            // you must block the main thread after
            // starting a task (by waiting the task or
            // by calling `Console.ReadLine`).
            ReadLine();

            DisplayCurrentThreadInfo("Exiting");

        }

        /// <summary>
        /// Demonstrates usage of threads.
        /// </summary>
        static void ThreadingDemo()
        {
            Threading.Basics.Demo.Test();
        }

        /// <summary>
        /// Demonstrates usage of tasks.
        /// </summary>
        static void TasksDemo()
        {
            Tasks.Basics.Demo.Test();
            WriteLine();

            Tasks.TCS.Demo.Test();
        }

        /// <summary>
        /// Demonstrates usage of ansychronous programming.
        /// </summary>
        static async Task AsynchronousProgrammingDemoAsync()
        {
            //await AsynchronousProgramming.Basics.Demo.TestAsync();
            //WriteLine();

            await AsynchronousProgramming.Functions.Demo.TestAsync();
        }
    }
}
