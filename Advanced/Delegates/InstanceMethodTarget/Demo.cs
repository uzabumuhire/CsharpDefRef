using static System.Console;

using static Core.ConsoleHelper;

namespace Advanced.Delegates.InstanceMethodTarget
{
    static class Demo
    {
        /// <summary>
        /// Demonstrate how to use delegates with instance method targets.
        /// </summary>
        internal static void Test()
        {
            // 
            ProgressReportWriter writer = new ProgressReportWriter();
            ProgressReporter p = writer.WriteProgressToConsole;

            for (int i = 1; i < 101; i++)
            {
                p(i);
                System.Threading.Thread.Sleep(100);
            }
            CursorLeft += 19;

            // The delegate instance's `Target` property represents the instance
            // that contains the method that was assigned. `Target` property will
            // `null` for a delegate referencing a static method.
            DisplayBar();
            Write(p.Target == writer); // True
            DisplayBar();
            Write(p.Method); // Void WriteProgressToConsle(Int32)
        }
    }
}
