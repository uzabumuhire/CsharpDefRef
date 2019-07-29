using System;
using System.Collections;

using static System.Console;

using static Core.ConsoleHelper;

namespace Collections.Lists.Arrays
{
    static class Demo
    {
        /// <summary>
        /// Demontrates usage of arrays.
        /// </summary>
        internal static void Run()
        {
            // Tests accessing arrays via `IList` indexer.
            ArrayListIndexers();

            WriteLine();

            // Tests arrays structural equality comparison.
            ArrayEqualityComparison();

            WriteLine();

            // Tests arrays creation and indexing.
            ArrayConstructionIndexing();

            WriteLine();

            // Tests arrays enumeration.
            ArrayEnumeration();

            WriteLine();

            // Tests arrays searching.
            ArraySearch();

            WriteLine();

            // Tests arrays sorting.
            ArraySort();

            WriteLine();

            // Tests arrays converting.
            ArrayConvert();

        }

        /// <summary>
        /// Tests accessing arrays via <see cref="IList"/> indexer.
        /// </summary>
        internal static void ArrayListIndexers()
        {
            object[] a1 = { "string", 123, true };
            FirstOrNull(a1);
            Write(FirstOrNull(a1));        // string
            DisplayBar();

            object[][] a2 = {
                new object[]{ "string", 123, true },
                new object[]{ "string", 123, true } };
            Write(FirstOrNull(a2));        // null
        }

        /// <summary>
        /// Protects against accesssing a multi-dimensional array via
        /// <see cref="IList"/> indexer.
        /// </summary>
        /// <returns>The or null.</returns>
        /// <param name="l">L.</param>
        static object FirstOrNull(IList l)
        {
            // An `ArgumentException` is thrown if you try to access
            // a multi-dimensional array via `IList` indexer. Test for 
            // a multi-dimensional array at runtime with this expression.
            if (!(l.GetType().IsArray && l.GetType().GetArrayRank() > 1))
            {
                if (l == null || l.Count == 0)
                    return null;
                return l[0];
            }
            return null;
        }

        /// <summary>
        /// Compares arrays equality structurally by using the
        /// custom equaliy comparer <see cref="StructuralComparisons"/>.
        /// </summary>
        internal static void ArrayEqualityComparison()
        {
            object[] a1 = { "string", 123, true };
            object[] a2 = { "string", 123, true };

            // Two distinct arrays will always fail an equality test - unless
            // you use a custom equality comparer.
            Write(a1 == a2);        // False
            DisplayBar();
            Write(a1.Equals(a2));   // False
            DisplayBar();

            // Use of a custom equality comparer `StructuralComparisons`
            // to compare elements in an array.
            IStructuralEquatable se1 = a1;
            Write(se1.Equals(a2,
                StructuralComparisons.StructuralEqualityComparer)); // True
        }

        /// <summary>
        /// Demonstrates arrays construction and indexing.
        /// </summary>
        internal static void ArrayConstructionIndexing()
        {
            // Create and index arrays through C#'s language constructs.
            int[] myArray = { 1, 2, 3 };
            DisplayBar();
            Write(
                "First element : {0} Last element {1}", myArray[0], myArray[1]);

            // Instantiate arrays dynamically by calling `Array.CreateInstance`
            // and specifying element type and rank (number of dimensions)
            // but also allowing nonzero-based arrays through specifying a 
            // a lower bound. Nonzero-based arrays are not CLS (Common Language
            // Specification) compliant.
            Array a = Array.CreateInstance(typeof(string), 2); // string[] a = new string[2]
            a.SetValue("hi", 0); // a[0] = "hi"
            a.SetValue("there", 1); // a[1] = "there"
            string s1 = (string)a.GetValue(0); // s = a[0]
            DisplayBar();
            Write(s1);

            // Casting to a C# array. Zero-indexed arrays created dynamically 
            // can be cast to a C# array of a matching or compatible type
            // (compatible by standard array-variance rules)
            string[] cSharpArray = (string[])a;
            string s2 = cSharpArray[0];

            // First element of any array regardless of rank.
            int[] a2 = { 9, 2, 3 };
            int[,] a3 = { { 5, 6 }, { 7, 8 } };
            DisplayFirstValue(a2);
            DisplayFirstValue(a3);
            DisplayFirstValue<int>(a2);
        }

        /// <summary>
        /// <see cref="Array.GetValue(int[])"/> and <see cref="Array.SetValue(object, int[])"/>
        /// are useful when writing methods that can deal an array  of any type and rank.
        /// For multidimensional arrays, they accept an array of indexers.
        /// </summary>
        /// <param name="a">A given array.</param>
        static void DisplayFirstValue(Array a)
        {
            DisplayBar();
            Write(a.Rank + "-dimensional");

            // The indexers array will automatically initialize to all zeros,
            // so passing it into `GetValue` or `SetValue` will get/set the
            // zero based (i.e, first) element int the array.
            int[] indexers = new int[a.Rank];
            DisplayBar();
            Write(a.GetValue(indexers));
        }

        /// <summary>
        /// Working with arrays of unknown type but known rank,
        /// generics provice a more efficient solution.
        /// </summary>
        /// <param name="array">A given array.</param>
        /// <typeparam name="T">The type of the element in <paramref name="array"/>.</typeparam>
        static void DisplayFirstValue<T>(T[] array)
        {
            DisplayBar();
            Write(array[0]);
        }

        /// <summary>
        /// Testing arrays enumeration/iteration.
        /// </summary>
        internal static void ArrayEnumeration()
        {
            // Enumerating using `foreach`.
            int[] myArray = { 1, 2, 3 };
            foreach (int val in myArray)
            {
                Write(" " + val);
            }

            // Enumerate using a static 
            // Array.ForEach<T>(T[] array, Action<T> action).
            // public delagate static void Action<T>(T obj).
            Array.ForEach(new[] { 1, 2, 3 }, DisplaySpaceVal);

        }

        /// <summary>
        /// Search an array of strings for a name containing the letter a..
        /// </summary>
        internal static void ArraySearch()
        {
            string[] names = { "Rodney", "Jack", "Jill" };
            //string match = Array.Find(names, delegate (string name) { return name.Contains("a"); }); // using an anonymous method.
            //string match = Array.Find(names, n => n.Contains("a")); // using a lambda expression.
            string match = Array.Find(names, ContainsA);
            Write(match);
        }

        static bool ContainsA(string name)
        {
            return name.Contains("a");
        }

        /// <summary>
        /// Testing sorting arrays.
        /// </summary>
        internal static void ArraySort()
        {
            // Sorting a single array.
            int[] numbers1 = { 3, 2, 1 };
            Array.Sort(numbers1);
            Array.ForEach(numbers1, DisplaySpaceVal);

            // Sorting a pair of arrays.
            // The methods accepting a pair of arrays works by rearranging the
            // items of each array in tandem, basing ordering on the first 
            // array.
            int[] numbers2 = { 7, 6, 5 };
            string[] words = { "seven", "six", "five" };
            Array.Sort(numbers2, words);
            Array.ForEach(words, DisplaySpaceVal);

            // Sorting with a custom comparison provider (comparison delegate)
            // Sorting an array of integers such that odd numbers come first.
            int[] numbers3 = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Array.ForEach(numbers3, DisplaySpaceVal);
            Array.Sort(
                numbers3,
                (x, y) => x % 2 == y % 2 ? 0 : x % 2 == 1 ? -1 : 1
                );
            Array.ForEach(numbers3, DisplaySpaceVal);
        }

        /// <summary>
        /// Testing arrays converters.
        /// </summary>
        internal static void ArrayConvert()
        {
            float[] reals = { 1.3f, 1.5f, 1.8f };
            Array.ForEach(reals, DisplaySpaceVal);

            // Converts an array of floats to an array of integers.
            int[] wholes = Array.ConvertAll(reals, r => Convert.ToInt32(r));

            Array.ForEach(wholes, DisplaySpaceVal);
        }

    }
}
