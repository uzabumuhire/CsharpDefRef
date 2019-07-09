using System.Threading;

using static Core.ConsoleHelper;

namespace Core
{
    public static class ThreadHelper
    {
        public static void DisplayCurrentThreadInfo(string message)
        {
            DisplayThreadInfo(message + " " + Thread.CurrentThread.Name);
        }
    }
}
