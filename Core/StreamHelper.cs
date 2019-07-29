using System.IO;

using static System.Console;

using static Core.ConsoleHelper;

namespace Core
{
    public static class StreamHelper
    {
        /// <summary>
        /// Displays information about a given stream.
        /// </summary>
        /// <param name="s">A given stream.</param>
        public static void DisplayStreamInfo(Stream s)
        {
            DisplaySpaceVal(s.CanRead ? "readable" : "unreadable");
            DisplaySpaceVal(s.CanWrite ? "writable" : "unwritable");
            DisplaySpaceVal(s.CanSeek ? "seekable" : "readable");
            DisplaySpaceVal(s.CanTimeout ? "timeout/available" : "timeout/unvailable");
            WriteLine();
        }
    }
}
