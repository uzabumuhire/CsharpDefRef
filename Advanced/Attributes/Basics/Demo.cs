using System.Runtime.CompilerServices;

using static Core.Utility;

namespace Advanced.Attributes.Basics
{
    static class Demo
    {
        internal static void Test()
        {
            CallerInfoUsage();
        }

        /// <summary>
        /// Demonstrate usage of the caller info attributes.
        /// </summary>
        /// <param name="memberName">The caller's member name.</param>
        /// <param name="filePath">The path to caller's source code file.</param>
        /// <param name="lineNumber">The line number in caller's source code file.</param>
        static void CallerInfoUsage(
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            DisplaySpaceVal(memberName);
            DisplayBar();
            DisplaySpaceVal(filePath);
            DisplayBar();
            DisplaySpaceVal(lineNumber);
        }
    }
}
