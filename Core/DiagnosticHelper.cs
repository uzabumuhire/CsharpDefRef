using System.Diagnostics;
using System.Runtime.CompilerServices;

using static Core.ConsoleHelper;

namespace Core
{
    public static class DiagnosticHelper
    {
        public static int TrackingId = 0;

        public static void DisplayCurrentMethodInfo(
            string message,
            int trackingId,
            [CallerMemberName] string memberName = null)
        {
            DisplayMethodInfo(memberName + "_" + trackingId + " " + message);
        }
    }
}
