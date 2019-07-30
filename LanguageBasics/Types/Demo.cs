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

            WriteLine();

            CustomTypes();
        }

        /// <summary>
        /// Demonstrates basic usage of custom types.
        /// </summary>
        static void CustomTypes()
        {
            // Data is created by *instantiating* a type.

            // Predefined types can be instantiated simply
            // by using a literal (such as 12 or "Hello world").

            // The `new` operator creates instances of a custom type.
            // Immediately after the `new` operator instantiates an
            // object, the object's *constructor* is called to perform
            // initialization.

            // Creates and declares instances of the `UnitConverter` type.
            UnitConverter feetToInchesConverter = new UnitConverter(12);
            UnitConverter milesToFeetConverter = new UnitConverter(5280);

            DisplayVal(feetToInchesConverter.Convert(30), " | "); // 360
            DisplayVal(feetToInchesConverter.Convert(100), " | "); // 1200
            DisplayVal(feetToInchesConverter.Convert(
                milesToFeetConverter.Convert(1)), " | "); // 63360

            // Creates two `Panda`, prints their names
            // and then their total population.
            Panda p1 = new Panda("Pan Dee");
            Panda p2 = new Panda("Pan Dah");

            DisplayVal(p1.Name, " | ");
            DisplayVal(p2.Name, " | ");

            WriteLine($"The total population of pandas : {Panda.Population}");
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
