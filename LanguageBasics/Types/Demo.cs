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

            WriteLine();

            Conversions();
        }

        /// <summary>
        /// Demonstrates basic usage of conversions.
        /// </summary>
        static void Conversions()
        {
            // C# can convert between instances of compatible types.

            // A *conversion* always creates an new value from an
            // existing one. Conversions can be either *implicit*
            // or *explicit*.

            // If the compiler can determine that a conversion will
            // *always* fail, both kinds of conversion are prohibited.

            // The following *numeric conversions* are built into the
            // language.

            // C# also support :
            // - *reference conversions*
            // - *boxing/unboxing conversions*
            // - *custom conversions*

            // The compiler doesn't enforce the following rules with
            // custom conversions, so it's possible for badly designed
            // types to behave otherwise.

            // Implicit conversions happen automatically.

            // Implicit conversions are allowed when both of the 
            // following are true:
            // - the compiler can guarantee they will always succeed.
            // - no information is lost in conversion. (A minor caveat
            //   is that very large `long` values loose information
            //   when converted to `double`).

            int x = 12345; // int a 32-bit integer
            DisplayBarVal(int.MaxValue);
            DisplayBarVal(x);

            // Implicitly converts an `int` to a `long` (which has
            // twice the bitwise capacity of an `int`).
            long y = x; // Implicit conversion to 64-bit integer
            DisplayBarVal(long.MaxValue);
            DisplayBarVal(y);

            // Explicit conversions require a *cast*

            // Explicit conversions are required when the following
            // is true :
            // - the compiler cannot guarantee they will always
            //   succeed.
            // - information may be lost  during converision.

            // Explicitly cast an `int` to a `short` (which has
            // half the capacity of an `int`)
            short z = (short)x; // Explicit conversion to 16-bit integer.
            DisplayBarVal(short.MaxValue);
            WriteLine(z);
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

            DisplayBarVal(feetToInchesConverter.Convert(30)); // 360
            DisplayBarVal(feetToInchesConverter.Convert(100)); // 1200
            DisplayBarVal(feetToInchesConverter.Convert(
                milesToFeetConverter.Convert(1))); // 63360

            // Creates two `Panda`, prints their names
            // and then their total population.
            Panda p1 = new Panda("Pan Dee");
            Panda p2 = new Panda("Pan Dah");

            DisplayBarVal(p1.Name);
            DisplayBarVal(p2.Name);

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
            DisplayBarVal(x);

            // A *constant* always represents the same value.
            const int y = 360;
            DisplayBarVal(y);

            // `string` is a predefined type for representing
            // a sequence of characters.

            // Work with strings by calling functions on them.
            string message = "Hello world";
            string upperMessage = message.ToUpper();
            DisplayBarVal(upperMessage);

            message = message + x.ToString();
            DisplayBarVal(message);

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
