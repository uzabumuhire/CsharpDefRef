using static System.Console;

namespace Types.Enums
{
    static class Demo
    {
        /// <summary>
        /// Demonstrates usage of the <c>enum</c> type.
        /// </summary>
        internal static void Test()
        {
            BorderSideDefault topSide = BorderSideDefault.Top;
            WriteLine("Is top : {0}", topSide == BorderSideDefault.Top);
        }
    }
}
