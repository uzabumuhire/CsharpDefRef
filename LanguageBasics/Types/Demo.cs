using static System.Console;

using static Core.ConsoleHelper;

namespace Basics.Types
{
    static class Demo
    {
        /// <summary>
        /// Demonstrates basic usage of types.
        /// </summary>
        internal static void Run()
        {
            Fundamentals();
        }

        /// <summary>
        /// Demonstrates fundementals of types.
        /// </summary>
        static void Fundamentals()
        {
            // A *type* defines the blueprint for a value.

            // Predefined types are types that are specially
            // supported by the compiler.

            // In C#, predefined types (also referred to as
            // built-in types) are recognized with a C#
            // keyword. The `System` namespace in the .NET
            // Framework contains many important types that
            // are not predefined by C# (e.g., `DateTime`).

            // `int` is a predefined type for representing the set 
            // of integers that fit into 32 bits of memory, from
            // -2^31 to (2^31)-1 and is the default type for 
            // numeric literals within this range.

            // All values in C# are *instances* of a type. The
            // meaning of a value, and the set of possible values
            // a variable can have, is determined by its type.

            // A *variable* denotes a storage location that
            // can contain different values over time.

            // Declaring a variable of type `int` and performing
            // function with instances of the `int` type.
            int x = 12 * 30;
            DisplayVal(x, " | ");

            // A *constant* always represents the same value.
            const int y = 360;
            DisplayVal(y, " | ");

            // `string` is a predefined type for representing
            // a sequence of characters.

            // Work with strings by calling functions on them.
            string message = "Hello world";
            string upperMessage = message.ToUpper();
            DisplayVal(upperMessage, " | ");

            message = message + x.ToString();
            DisplayVal(message, " | ");

            // `bool` is a predefined type that has exactly two 
            // values : `true` and `false`. The `bool` type is 
            // commonly used to conditionally branch execution
            // flow based with an `if` statement.
            bool lessThanAMile = x < 5280;
            if(lessThanAMile)
                WriteLine("This will print");
        }
    }
}
