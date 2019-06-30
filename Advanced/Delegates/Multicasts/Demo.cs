using System.IO;

using static System.Console;

namespace Advanced.Delegates.Multicasts
{
    static class Demo
    {
        /// <summary>
        /// Demonstrates the use of multicast delegates to monitor progress.
        /// </summary>
        internal static void Test()
        {
            // A multicast delegate instance used so that progress
            // is monitored by two independent methods `WriteProgressToConsole`
            // and `WriteProgressToFile`.
            ProgressRepoter p = WriteProgressToConsole;

            //p = p + WriteProgressToFile;
            p += WriteProgressToFile;

            Worker.HardWork(p);
        }

        /// <summary>
        /// Writes the progress to console.
        /// </summary>
        /// <param name="percentComplete">The percentage of completed work.</param>
        static void WriteProgressToConsole(int percentComplete, TextWriter writer = null) 
        {
            Write(string.Format("Progress... {0, 5} %", percentComplete));
            CursorLeft -= 19;
        }

        /// <summary>
        /// Writes the progress to file.
        /// </summary>
        /// <param name="percentComplete">The percentage of completed work.</param>
        static void WriteProgressToFile(int percentComplete, TextWriter writer)
        {
            writer.WriteLine(string.Format("Progress... {0, 5} %", percentComplete));
        }
    }
}
