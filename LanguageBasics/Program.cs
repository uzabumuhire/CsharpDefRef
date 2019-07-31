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
            // TYPES
            DisplayDemo("TYPES", TypesDemo);

            // NUMERICS
            DisplayDemo("NUMERICS", NumericsDemo);

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

        /// <summary>
        /// Demonstrates basic usage of numeric types.
        /// </summary>
        static void NumericsDemo()
        {
            Numerics.Demo.Run();
        }

        /// <summary>
        /// Demonstrates basic usage of types.
        /// </summary>
        static void TypesDemo()
        {
            Types.Demo.Run();
        }
    }
}
