using System;

using static System.Console;

using static Core.ConsoleHelper;

namespace Basics.Arrays
{
    static class Demo
    {
        internal static void Run()
        {
            Basics();

            WriteLine();

            DefaultElementInitialization();
        }

        static void Basics()
        {
            // An *array* represents a fixed number of
            // variables (called *elements*) of a
            // particular type.

            // All arrays inherit from the `System.Array`
            // class, providing common services for all
            // arrays.

            // An array itself is always a reference type
            // object regardless of the element type.
            //int[] a = null;

            // The elements in a array are always stored
            // in a contiguous block of memory, providing
            // highly efficient access.

            // An array is denoted with square brackets
            // after the element type.
            // char[] vowels = new char[5];

            // Square brackets also *index* the array,
            // accessing a particular element by position.
            /*
            vowels[0] = 'a';
            vowels[1] = 'e';
            vowels[2] = 'i';
            vowels[3] = 'o';
            vowels[4] = 'u';

            // The following prints 'e' since array indexes
            // start at 0.
            Console.Write(vowels[1]); // e
            */

            // *Array initialization expression* let you
            // declare and populate an array in a single
            // step.
            //char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'i' };
            char[] vowels = { 'a', 'e', 'i', 'o', 'i' };

            // The `Length` property returns the size
            // of the array (the number of elements) in
            // the array. Once an array has been created
            // its length, cannot be changed.

            // Using the `for` loop statement to
            // iterate through the array.
            for (int i = 0; i < vowels.Length; i++)
                DisplaySpaceVal(vowels[i]);

        }

        static void DefaultElementInitialization()
        {
            // Creating an array always preinitializes
            // the elements with default values. The
            // default value for a type is the result
            // of a bitwise zeroing of memory.

            // Create an array of 1,000 integers, since
            // `int` is a value type this allocates
            // 1,000 integers in one contiguous block
            // of memory. The default value for each
            // element will be 0.
            int[] a1 = new int[1000];
            DisplayVal(a1[123], " | "); // displays 0

            // Whether an array element type is a value
            // type or a reference type has important
            // performance implications.

            // When the element type is a value type,
            // each element value is allocated as part
            // of the array.
            PointValue[] a2 = new PointValue[1000];
            DisplayVal(a2[500].X, ""); // diplays 0
            WriteLine();

            // Allocates 1,000 null references, because
            // the element type is a reference type.
            PointReference[] a3 = new PointReference[1000];
            try
            {
                // Since `a3[500]` is `null`
                // the following will produce
                // a `NullReferenceException`.
                DisplayVal(a3[500].X, " | "); 
            }
            catch (NullReferenceException ex)
            {
                DisplayError(ex.ToString());
            }

            // To avoid the `NullReferenceException` runtime
            // error, we must explicitly instantiante 1,000
            // `PointReference` after instantiating the array.
            for (int i = 0; i < a3.Length; i++)
                a3[i] = new PointReference();

            DisplayVal(a3[500].X, "");


        }
    }
}
