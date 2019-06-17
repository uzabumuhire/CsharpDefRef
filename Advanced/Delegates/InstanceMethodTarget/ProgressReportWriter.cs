using static System.Console;

namespace Advanced.Delegates.InstanceMethodTarget
{
    internal delegate void ProgressReporter(int percentComplete);

    class ProgressReportWriter
    {
        public void WriteProgressToConsole(int percentComplete)
        {
            Write(string.Format("Progress... {0, 5} %", percentComplete));
            CursorLeft -= 19;
        }
    }
}
