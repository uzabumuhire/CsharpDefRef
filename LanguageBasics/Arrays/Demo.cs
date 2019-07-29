using System;

using static System.Console;

using static Core.MatrixHelper;
using static Core.ConsoleHelper;

namespace Basics.Arrays
{
    static class Demo
    {
        /// <summary>
        /// Demonstrates basic usage of arrays.
        /// </summary>
        internal static void Run()
        {
            Basics();

            WriteLine();

            DefaultElementInitialization();

            WriteLine();

            RectangularArrays();

            WriteLine();

            JaggedArrays();

            WriteLine();

            BoundsChecking();
        }

        static void BoundsChecking()
        {
            // All array indexing is bounds-checked by the runtime.
            // An `IndexOutOfRangeException` is thrown if you use
            // an invalid index. Array bounds checking is necessary
            // for type safety and simplifies debugging. C# provides
            // *unsafe* code that can explicitly bypass bounds
            // checking.
            
            int[] a = new int[3];

            try
            {
                // `IndexOutOfRangeException`
                // thrown at runtime.
                a[3] = 1; 
            }
            catch (IndexOutOfRangeException ex)
            {
                DisplayError(ex.ToString());
            }
        }

        /// <summary>
        /// Demonstrates usage of jagged arrays.
        /// </summary>
        static void JaggedArrays()
        {
            // *Jagged arrays* are arrays of arrays.

            // Jagged arrays are declared using successive
            // square brackets to represent each dimension.

            // Declares a jagged two-dimensional array,
            // where the outermost dimension is 3.
            int[][] matrix1 = new int[3][];

            // The inner dimensions aren't specified in the
            // declaration because, unlike a rectangular array,
            // each inner array can be an arbitrary length.
            // Each inner array is implicitly initialized to
            // `null` rather than an empty array. Each inner
            // array must be created manually.
            for (int i = 0; i < matrix1.Length; i++)
            {
                matrix1[i] = new int[3]; // create inner array
                for (int j = 0; j < matrix1[i].Length; j++)
                    matrix1[i][j] = i * 3 + j;
            }

            Display2DJaggedMatrix(matrix1);
            WriteLine();

            Preview2DJaggedMatrix(matrix1, 6);
            WriteLine();

            /*
            // A jagged array can be initialized.
            int[][] matrix2 = new int[][]
            {
                new int[] {0, 1, 2},
                new int[] {3, 4, 5},
                new int[] {6, 7, 8}
            };
            */

            // Shortens array initialization expressions by
            // omitting the `new` operator and
            // type qualifications.
            int[][] matrix2 =
            {
                new int[] {0, 1, 2},
                new int[] {3, 4, 5},
                new int[] {6, 7, 8}
            };

            Display2DJaggedMatrix(matrix2);
            WriteLine();

            // `matrix3` is implicitly type to `int[][]`.
            var matrix3 = new int[][]
            {
                new int[] {0, 1, 2},
                new int[] {3, 4, 5},
                new int[] {6, 7, 8}
            };

            Display2DJaggedMatrix(matrix3);
        }

        /// <summary>
        /// Demonstrates usage of rectangular arrays.
        /// </summary>
        static void RectangularArrays()
        {
            // *Rectangular arrays* represent an n-dimensional
            // block of memory.

            // Rectangular arrays are declared using commas to
            // separate each dimension.

            // Declares a rectangular two-dimensional array,
            // where the dimensions are 3 by 3.
            int[,] matrix1 = new int[3, 3];

            // `GetLength` method of an array returns the length
            // for a given dimension (starting at 0).
            for (int i = 0; i < matrix1.GetLength(0); i++)
                for (int j = 0; j < matrix1.GetLength(1); j++)
                    matrix1[i, j] = i * 3 + j;

            Display2DMatrix(matrix1);
            WriteLine();

            Preview2DMatrix(matrix1, 6);
            WriteLine();

            /*
            // A rectangular array can be initialized.
            int[,] matrix2 = new int[,]
            {
                {0, 1, 2 },
                {3, 4, 5 },
                {6, 7, 8 }
            };
            */

            // Shortens array initialization expressions by
            // omitting the `new` operator and
            // type qualifications.
            int[,] matrix2 = 
            {
                {0, 1, 2 },
                {3, 4, 5 },
                {6, 7, 8 }
            };

            Display2DMatrix(matrix2);
            WriteLine();

            // `matrix3` is implicitly typed to int[,].
            var matrix3 = new int[,]
            {
                {0, 1, 2 },
                {3, 4, 5 },
                {6, 7, 8 }
            };

            Display2DMatrix(matrix3);
        }

        /// <summary>
        /// Demonstrates array default element initialization.
        /// </summary>
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

            DisplayVal(a3[500].X, ""); // displays 0
        }

        /// <summary>
        /// Demonstrates basics arrays usage.
        /// </summary>
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
            //char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'u' };

            // Shortens array initialization expressions by
            // omitting the `new` operator and
            // type qualifications.
            char[] vowels1 = { 'a', 'e', 'i', 'o', 'u' };

            Preview1DMatrix(vowels1, 3);
            WriteLine();

            // The `Length` property returns the size
            // of the array (the number of elements) in
            // the array. Once an array has been created
            // its length, cannot be changed.

            // Using the `for` loop statement to
            // iterate through the array.
            for (int i = 0; i < vowels1.Length; i++)
                DisplaySpaceVal(vowels1[i]);

            WriteLine();

            // The `var` keyword tells the compiler to 
            // implicitly type a local variable.
            var vowels2 = new char[] { 'a', 'e', 'i', 'o', 'u' };
            Display1DMatrix(vowels2);
            WriteLine();

            // Implicit typing can be taken one stage further
            // with arrays by omitting the type qualifier
            // after the `new` keyword and have the compiler
            // *infer* the array type.
            var vowels3 = new[] { 'a', 'e', 'i', 'o', 'u' };
            Display1DMatrix(vowels3);
            WriteLine();

            // For this the code above to work, the elements
            // must all be implicitly convertible to a single
            // type (and at least one of the element must be
            // that type, and there must be exactly one best
            // type).
            var x = new[] { 1, 10_000_000_000 }; // all convertible to `long`
            Display1DMatrix(x);
        }
    }
}
