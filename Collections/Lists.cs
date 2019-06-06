using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;


namespace Collections
{
    static class Lists
    {
        // Tests accessing arrays via `IList` indexer.
        internal static void ArrayListIndexers()
        {
            object[] a1 = { "string", 123, true };
            FirstOrNull(a1);
            Console.Write(FirstOrNull(a1));        // string
            Console.Write(" | ");

            object[][] a2 = { 
                new object[]{ "string", 123, true },
                new object[]{ "string", 123, true } };
            Console.Write(FirstOrNull(a2));        // null
        }

        // Protects against accesssing a multi-dimensional array via
        // `IList` indexer.
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

        // Compares arrays equality structurally by using the
        // custom equaliy comparer `StructuralComparisons`.
        internal static void ArrayEqualityComparison()
        {
            object[] a1 = { "string", 123, true };
            object[] a2 = { "string", 123, true };

            // Two distinct arrays will always fail an equality test - unless
            // you use a custom equality comparer.
            Console.Write(a1 == a2);        // False
            Console.Write(" | ");
            Console.Write(a1.Equals(a2));   // False
            Console.Write(" | ");

            // Use of a custom equality comparer `StructuralComparisons`
            // to compare elements in an array.
            IStructuralEquatable se1 = a1;
            Console.Write(se1.Equals(a2,
                StructuralComparisons.StructuralEqualityComparer)); // True
        }

        // Demonstrating arrays construction and indexing.
        internal static void ArrayConstructionIndexing()
        {
            // Create and index arrays through C#'s language constructs.
            int[] myArray = { 1, 2, 3 };
            Console.Write(
                " | First element : {0} Last element {1}", 
                myArray[0], 
                myArray[1]);

            // Instantiate arrays dynamically by calling `Array.CreateInstance`
            // and specifying element type and rank (number of dimensions)
            // but also allowing nonzero-based arrays through specifying a 
            // a lower bound. Nonzero-based arrays are not CLS (Common Language
            // Specification) compliant.
            Array a = Array.CreateInstance(typeof(string), 2); // string[] a = new string[2]
            a.SetValue("hi", 0); // a[0] = "hi"
            a.SetValue("there", 1); // a[1] = "there"
            string s1 = (string) a.GetValue(0); // s = a[0]
            Console.Write(" | " + s1);

            // Casting to a C# array. Zero-indexed arrays created dynamically 
            // can be cast to a C# array of a matching or compatible type
            // (compatible by standard array-variance rules)
            string[] cSharpArray = (string[])a;
            string s2 = cSharpArray[0];

            // First element of any array regardless of rank.
            int[] a2 = { 9, 2, 3 };
            int[,] a3 = { { 5, 6 }, { 7, 8 } };
            PrintFirstValue(a2);
            PrintFirstValue(a3);
            PrintFirstValue<int>(a2);
        }

        // `GetValue` and `SetValue` are useful when writing methods that can deal
        // an array  of any type and rank. For multidimensional arrays, they
        // accept an array of indexers.
        static void PrintFirstValue(Array a)
        {
            Console.Write(" | " + a.Rank + "-dimensional");

            // The indexers array will automatically initialize to all zeros,
            // so passing it into `GetValue` or `SetValue` will get/set the
            // zero based (i.e, first) element int the array.
            int[] indexers = new int[a.Rank];
            Console.Write(" | " + a.GetValue(indexers));
        }

        // Working with arrays of unknown type but known rank,
        // generics provice a more efficient solution.
        static void PrintFirstValue<T>(T[] array)
        {
            Console.Write(" | " + array[0]);
        }

        // Enumerating arrays.
        internal static void ArrayEnumeration()
        {
            // Enumerating using `foreach`.
            int[] myArray = { 1, 2, 3 };
            foreach (int val in myArray)
            {
                Console.Write(" " + val);
            }

            // Enumerate using a static 
            // Array.ForEach<T>(T[] array, Action<T> action).
            // public delagate static void Action<T>(T obj).
            Array.ForEach(new[] { 1, 2, 3 }, WriteSpaceVal);

        }

        // Search an array of strings for a name containing the letter a.
        internal static void ArraySearch()
        {
            string[] names = { "Rodney", "Jack", "Jill" };
            //string match = Array.Find(names, delegate (string name) { return name.Contains("a"); }); // using an anonymous method.
            //string match = Array.Find(names, n => n.Contains("a")); // using a lambda expression.
            string match = Array.Find(names, ContainsA);
            Console.Write(match);
        }

        static bool ContainsA(string name)
        {
            return name.Contains("a");
        }

        // Sorting arrays
        internal static void ArraySort()
        {
            // Sorting a single array.
            int[] numbers1 = { 3, 2, 1 };
            Array.Sort(numbers1);
            Array.ForEach(numbers1, WriteSpaceVal);

            // Sorting a pair of arrays.
            // The methods accepting a pair of arrays works by rearranging the
            // items of each array in tandem, basing ordering on the first 
            // array.
            int[] numbers2 = { 7, 6, 5 };
            string[] words = { "seven", "six", "five" };
            Array.Sort(numbers2, words);
            Array.ForEach(words, WriteSpaceVal);

            // Sorting with a custom comparison provider (comparison delegate)
            // Sorting an array of integers such that odd numbers come first.
            int[] numbers3 = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Array.ForEach(numbers3, WriteSpaceVal);
            Array.Sort(
                numbers3, 
                (x, y) => x % 2 == y % 2 ? 0 : x % 2 == 1 ? -1 : 1
                );
            Array.ForEach(numbers3, WriteSpaceVal);
        }


        // Converting 
        internal static void ArrayConvert()
        {
            float[] reals = { 1.3f, 1.5f, 1.8f };
            Array.ForEach(reals, WriteSpaceVal);

            // Converts an array of floats to an array of integers.
            int[] wholes = Array.ConvertAll(reals, r => Convert.ToInt32(r));

            Array.ForEach(wholes, WriteSpaceVal);
        }

        static void WriteSpaceVal<T>(T val)
        {
            Console.Write(" " + val);
        }
    }
}