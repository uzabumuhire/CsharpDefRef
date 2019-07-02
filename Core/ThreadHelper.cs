using System.Threading;

using static Core.ConsoleHelper;

namespace Core
{
    public static class ThreadHelper
    {
        public static void DisplayCurrentThreadInfo(string m)
        {
            DisplayThreadInfo(m + " " + Thread.CurrentThread.Name);
        }
    }
}
