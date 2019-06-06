using System;
using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


namespace Collections
{
    static class Lists
    {

        // Testing Array class

        // Tests accessing arrays via `IList` indexer.
        internal static void ArrayListIndexers()
        {
            object[] a1 = { "string", 123, true };
            FirstOrNull(a1);
            Console.Write(FirstOrNull(a1));        // string
            DisplayBar();

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
            DisplayBar();
            Console.Write(a1.Equals(a2));   // False
            DisplayBar();

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
            DisplayBar();
            Console.Write(
                "First element : {0} Last element {1}", myArray[0], myArray[1]);

            // Instantiate arrays dynamically by calling `Array.CreateInstance`
            // and specifying element type and rank (number of dimensions)
            // but also allowing nonzero-based arrays through specifying a 
            // a lower bound. Nonzero-based arrays are not CLS (Common Language
            // Specification) compliant.
            Array a = Array.CreateInstance(typeof(string), 2); // string[] a = new string[2]
            a.SetValue("hi", 0); // a[0] = "hi"
            a.SetValue("there", 1); // a[1] = "there"
            string s1 = (string) a.GetValue(0); // s = a[0]
            DisplayBar();
            Console.Write(s1);

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

        // `GetValue` and `SetValue` are useful when writing methods that can deal
        // an array  of any type and rank. For multidimensional arrays, they
        // accept an array of indexers.
        static void DisplayFirstValue(Array a)
        {
            DisplayBar();
            Console.Write(a.Rank + "-dimensional");

            // The indexers array will automatically initialize to all zeros,
            // so passing it into `GetValue` or `SetValue` will get/set the
            // zero based (i.e, first) element int the array.
            int[] indexers = new int[a.Rank];
            DisplayBar();
            Console.Write(a.GetValue(indexers));
        }

        // Working with arrays of unknown type but known rank,
        // generics provice a more efficient solution.
        static void DisplayFirstValue<T>(T[] array)
        {
            DisplayBar();
            Console.Write(array[0]);
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

        // Testing List<T> class
        internal static void GenericListTest()
        {
            List<string> words = new List<string>();

            words.Add("melon");
            words.Add("avocado");
            words.AddRange(new[] { "banana", "plum" });
            words.ForEach(WriteSpaceVal);

            words.Insert(0, "lemon"); // Insert at start
            words.InsertRange(0, new[] { "peach", "nashi" }); // Insert at start
            DisplayBar();
            words.ForEach(WriteSpaceVal);

            DisplayBar();
            Console.Write(words[0]); // first element
            DisplayBar();
            Console.Write(words[words.Count - 1]); // last element

            List<string> subset = words.GetRange(1, 2); // 2nd -> 3rd words
            DisplayBar();
            subset.ForEach(WriteSpaceVal);

            string[] wordsArray = words.ToArray(); // Creates a new typed array
            DisplayBar();
            Array.ForEach(wordsArray, WriteSpaceVal);

            // Copy the first two elements to the end of an existing array.
            string[] existingArray = new string[1000];
            words.CopyTo(0, existingArray, 998, 2);

            // Converting to upper case.
            List<string> upperCaseWords = words.ConvertAll(s => s.ToUpper());
            DisplayBar();
            upperCaseWords.ForEach(WriteSpaceVal);

            // Converting to lengths.
            List<int> lengths = words.ConvertAll(s => s.Length);
            DisplayBar();
            lengths.ForEach(WriteSpaceVal);

            words.Remove("melon");
            words.RemoveAt(3);
            words.RemoveRange(0, 2); // Removes the first 2 elements
            DisplayBar();
            words.ForEach(WriteSpaceVal);

            // Remove all strings containing 'n'.
            words.RemoveAll(s => s.Contains("n"));
            DisplayBar();
            words.ForEach(WriteSpaceVal);
        }

        // Testing ArrayList class
        internal static void TestNonGenericList()
        {
            // ArrayList class requires casts.
            ArrayList al = new ArrayList();
            al.Add("hello");
            al.Add("world");
            string first = (string)al[0];
            Console.Write(first);

            string[] strArr = (string[])al.ToArray(typeof(string));
            DisplayBar();
            Array.ForEach(strArr, WriteSpaceVal);

            // `ArrayList` casts cannot be verified by the compiler,
            // the following compiles but fails at runtime.
            //int firstItem = (int)al[0]; 

            // Using `System.Linq.Enumerable` extension methods `Cast` and  
            // `ToList`, you  can convert an `ArrayList` to a generic `List`.
            ArrayList numbers = new ArrayList();
            numbers.AddRange(new[] { 1, 5, 9 });
            List<int> list = numbers.Cast<int>().ToList();
            DisplayBar();
            list.ForEach(WriteSpaceVal);
        }

        static void WriteSpaceVal<T>(T val)
        {
            Console.Write(val + " ");
        }

        static void DisplayBar()
        {
            Console.Write(" | ");
        }
    }
}