using static System.Console;

using static Core.ConsoleHelper;

namespace Collections
{
    class Program
    {
        /// <summary>
        /// Demonstrates usage of collections in C#.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // ENUMERATION
            DisplayDemo("ENUMERATION", EnumerationDemo);

        }

        /// <summary>
        /// Demonstrates usage of enumerations.
        /// </summary>
        static void EnumerationDemo()
        {
            Enumeration.Basics.Demo.Run();

            WriteLine();

            Enumeration.Custom.Demo.Run();
        }
    }
}
