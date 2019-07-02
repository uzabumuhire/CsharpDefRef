using System.Threading;

using static System.Console;

using static Core.ThreadHelper;

namespace ConcurrencyAsynchrony
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "MAIN";

            DisplayCurrentThreadInfo("Entering");

            // THREADING
            WriteLine("THREADING");
            WriteLine();
            ThreadingDemo();

            // TASKS
            WriteLine();
            WriteLine();
            WriteLine("TASKS");
            WriteLine();
            TasksDemo();

            // ASYNCHRONOUS PROGRAMMING
            WriteLine();
            WriteLine();
            WriteLine("ASYNCHRONOUS PROGRAMMING");
            WriteLine();
            AsynchronousProgrammingDemo();

            DisplayCurrentThreadInfo("Exiting");

        }

        static void ThreadingDemo()
        {
            Threading.Basics.Demo.Test();
        }

        static void TasksDemo()
        {

        }

        static void AsynchronousProgrammingDemo()
        {

        }
    }

    
}
