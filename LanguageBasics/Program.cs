using static Core.ConsoleHelper;

namespace Basics
{
    class Program
    {
        /// <summary>
        /// Demonstrates usage of C# language basics.
        /// </summary>
        /// <param name="args">Program arguments.</param>
        static void Main(string[] args)
        {
            // STRINGS
            DisplayDemo("STRINGS", StringsDemo);

            // ARRAYS
            DisplayDemo("ARRAYS", ArraysDemo);
        }

        /// <summary>
        /// Demonstrates basic usage of arrays.
        /// </summary>
        static void ArraysDemo()
        {
            Arrays.Demo.Run();
        }

        /// <summary>
        /// Demonstrates basic usage of strings and characters.
        /// </summary>
        static void StringsDemo()
        {
            Strings.Demo.Run();
        }
    }
}
