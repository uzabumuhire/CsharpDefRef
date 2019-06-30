﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


using static System.Console;

using static Core.ConsoleHelper;
using static Core.CollectionsHelper;


namespace Collections
{
    static class Lists
    {
        // Testing Array class

        /// <summary>
        /// Tests accessing arrays via `IList` indexer.
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
        /// `IList` indexer.
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
        /// custom equaliy comparer `StructuralComparisons`.
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
        /// `GetValue` and `SetValue` are useful when writing methods that can deal
        /// an array  of any type and rank. For multidimensional arrays, they
        /// accept an array of indexers.
        /// </summary>
        /// <param name="a">The alpha component.</param>
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
        /// <param name="array">Array.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
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

        /// <summary>
        /// Testing generic List class.
        /// </summary>
        internal static void GenericList()
        {
            List<string> words = new List<string>();

            words.Add("melon");
            words.Add("avocado");
            words.AddRange(new[] { "banana", "plum" });
            words.ForEach(DisplaySpaceVal);

            words.Insert(0, "lemon"); // Insert at start
            words.InsertRange(0, new[] { "peach", "nashi" }); // Insert at start
            DisplayBar();
            words.ForEach(DisplaySpaceVal);

            DisplayBar();
            Write(words[0]); // first element
            DisplayBar();
            Write(words[words.Count - 1]); // last element

            List<string> subset = words.GetRange(1, 2); // 2nd -> 3rd words
            DisplayBar();
            subset.ForEach(DisplaySpaceVal);

            string[] wordsArray = words.ToArray(); // Creates a new typed array
            DisplayBar();
            Array.ForEach(wordsArray, DisplaySpaceVal);

            // Copy the first two elements to the end of an existing array.
            string[] existingArray = new string[1000];
            words.CopyTo(0, existingArray, 998, 2);

            // Converting to upper case.
            List<string> upperCaseWords = words.ConvertAll(s => s.ToUpper());
            DisplayBar();
            upperCaseWords.ForEach(DisplaySpaceVal);

            // Converting to lengths.
            List<int> lengths = words.ConvertAll(s => s.Length);
            DisplayBar();
            lengths.ForEach(DisplaySpaceVal);

            words.Remove("melon");
            words.RemoveAt(3);
            words.RemoveRange(0, 2); // Removes the first 2 elements
            DisplayBar();
            words.ForEach(DisplaySpaceVal);

            // Remove all strings containing 'n'.
            words.RemoveAll(s => s.Contains("n"));
            DisplayBar();
            words.ForEach(DisplaySpaceVal);
        }

        /// <summary>
        /// Testing ArrayList class.
        /// </summary>
        internal static void NonGenericList()
        {
            // ArrayList class requires casts.
            ArrayList al = new ArrayList();
            al.Add("hello");
            al.Add("world");
            string first = (string)al[0];
            Write(first);

            string[] strArr = (string[])al.ToArray(typeof(string));
            DisplayBar();
            Array.ForEach(strArr, DisplaySpaceVal);

            // `ArrayList` casts cannot be verified by the compiler,
            // the following compiles but fails at runtime.
            //int firstItem = (int)al[0]; 

            // Using `System.Linq.Enumerable` extension methods `Cast` and  
            // `ToList`, you  can convert an `ArrayList` to a generic `List`.
            ArrayList numbers = new ArrayList();
            numbers.AddRange(new[] { 1, 5, 9 });
            List<int> list = numbers.Cast<int>().ToList();
            DisplayBar();
            list.ForEach(DisplaySpaceVal);
        }

        /// <summary>
        /// Testing generic LinkedList class.
        /// </summary>
        internal static void GenericLinkedList()
        {
            LinkedList<string> tune = new LinkedList<string>();

            tune.AddFirst("do");
            tune.AddLast("so");
            tune.AddAfter(tune.First, "re");
            tune.AddAfter(tune.First.Next, "mi");
            tune.AddBefore(tune.Last, "fa");
            DisplayCollectionWithSpace(tune);

            tune.RemoveFirst();
            tune.RemoveLast();
            DisplayBar();
            DisplayCollectionWithSpace(tune);

            LinkedListNode<string> miNode = tune.Find("mi");
            tune.Remove(miNode);
            DisplayBar();
            DisplayCollectionWithSpace(tune);

            tune.AddFirst(miNode);
        }

        /// <summary>
        /// Tests generic Queue class.
        /// </summary>
        internal static void GenericQueue()
        {
            Queue<int> q = new Queue<int>();

            q.Enqueue(10);
            q.Enqueue(20);

            DisplayCollectionWithSpace(q);

            int[] data = q.ToArray();
            DisplayBar();
            DisplayCollectionWithSpace(data);

            DisplayBar();
            Write(q.Count);
            DisplayBar();
            Write(q.Peek());
            DisplayBar();
            Write(q.Dequeue());
            DisplayBar();
            Write(q.Dequeue());
        }

        /// <summary>
        /// Tests the generic Stack class.
        /// </summary>
        internal static void GenericStack()
        {
            Stack<int> s = new Stack<int>();

            s.Push(1);
            s.Push(2);
            s.Push(3);

            DisplayCollectionWithSpace(s);
            DisplayBar();
            Write(s.Count);
            DisplayBar();
            Write(s.Peek());
            DisplayBar();
            Write(s.Pop());
            DisplayBar();
            Write(s.Pop());
            DisplayBar();
            Write(s.Pop());
        }

        /// <summary>
        /// Tests the generic HashSet class.
        /// </summary>
        internal static void GenericHashSet()
        {
            // Constructs a `HashSet<char>` from an existing collection.
            // The reason we pass `string` into `HashSet<char>` is because
            // implements `IEnumerable<char>`.
            HashSet<char> letters = new HashSet<char>("the quick brown fox");

            // Enumerate the collection (notice the abscence of duplicates).
            DisplayCollectionWithoutSpace(letters);

            // Tests membership.
            DisplayBar();
            Write(letters.Contains('t'));
            DisplayBar();
            Write(letters.Contains('j'));

            // Extracts all the vowels from our set of characters.
            HashSet<char> lettersSet1 = new HashSet<char>(letters);
            lettersSet1.IntersectWith("aeiou");
            DisplayBar();
            DisplayCollectionWithoutSpace(lettersSet1);

            // Strips all the vowels from the set.
            HashSet<char> lettersSet2 = new HashSet<char>(letters);
            lettersSet2.ExceptWith("aeiou");
            DisplayBar();
            DisplayCollectionWithoutSpace(lettersSet2);

            // Removes all but the elements that are unique to one set or the
            // other. 
            HashSet<char> lettersSet3 = new HashSet<char>(letters);
            lettersSet3.SymmetricExceptWith("the lazy brown fox");
            DisplayBar();
            DisplayCollectionWithoutSpace(lettersSet3);

        }

        /// <summary>
        /// Tests the generic `SortedSet` class.
        /// </summary>
        internal static void GenericSortedSet()
        {
            SortedSet<char> letters = new SortedSet<char>("the quick brOwn fox");
            DisplayCollectionWithoutSpace(letters);

            DisplayBar();

            // Obtain letters between 'f' and 'j'.
            foreach (char c in letters.GetViewBetween('f', 'j'))
            {
                Write(c);
            }

            DisplayBar();

            // Obtain only lower cases letters.
            foreach (char c in letters.GetViewBetween('a', 'z'))
            {
                Write(c);
            }
        }
    }
}