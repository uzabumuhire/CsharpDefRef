using System;

namespace Collections
{
    class Program
    {
        static void Main(string[] args)
        {
            // ENUMERATION
            Console.WriteLine("ENUMERATION");
            Console.WriteLine();
            TestEnumeration();

            // PLUGGING IN EQUALITY AND ORDER
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("PLUGGING IN EQUALITY AND ORDER");
            Console.WriteLine();
            TestPluginEqualityOrder();

            // DICTIONARIES
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("DICTIONARIES");
            Console.WriteLine();
            TestDictionaries();

            // LISTS
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("LISTS");
            Console.WriteLine();
            TestLists();

        }

        static void TestEnumeration()
        {
            // Enumeration for strings as characters of arrays
            Enumeration.EnumerateString1("1:Uzabumhire", " ");
            Console.WriteLine();
            Enumeration.EnumerateString2("2:Uzabumhire", " ");

            Console.WriteLine();

            // Enumeration for arrays 
            Enumeration.EnumerateArrays1<int>(new int[] { 26, 27, 28, 29, 30 });
            Console.WriteLine();
            Enumeration.EnumerateArrays2<int>(new int[] { 31, 32, 33, 34, 35 });

            Console.WriteLine();

            // Iterator for collections
            MyCollection c = new MyCollection();
            foreach (var item in c)
                Console.Write(item + " ");

            Console.WriteLine();

            // Iterator for generic collections
            MyGenericCollection gc = new MyGenericCollection();
            foreach (var item in gc)
                Console.Write(item + " ");

            Console.WriteLine();

            foreach (var item in Enumeration.IntegerGenerator())
                Console.Write(item + " ");

            Console.WriteLine();

            // Custom implementation of enumerator for collections
            MyIntList il = new MyIntList();
            foreach (var item in il)
                Console.Write(item + " ");

            Console.WriteLine();

            // Custom implementation of enumerator for generic collections
            MyGenericIntList gil = new MyGenericIntList();
            foreach (var item in gil)
                Console.Write(item + " ");
        }

        static void TestPluginEqualityOrder()
        {
            // Testing `EqualityComparer`
            PluginEqualityOrder.TestEqualityComparer();

            Console.WriteLine();

            // Testing how a `Comparer` is used to sort a `List`
            PluginEqualityOrder.TestComparer1();

            Console.WriteLine();

            // Testing how a `Comparer` is used in `SortedDictionary`
            PluginEqualityOrder.TestComparer2();

            Console.WriteLine();

            // Testing how a `StringComparer` uses Autralian English.
            PluginEqualityOrder.TestStringComparer2();

            Console.WriteLine();

            // Testing `IStructuralEquatable` with arrays of integers
            PluginEqualityOrder.TestStructuralEquality1();

            Console.WriteLine();

            // Testing `IStructuralEquatable` with arrays of strings
            PluginEqualityOrder.TestStructuralEquality2();
        }

        static void TestDictionaries()
        {
            // Testing how to use the `Dictionary<TKey, TValue>`
            Dictionaries.TestDictionary();

            Console.WriteLine();

            // Testing how to use the `SortedList<Tkey, TValue>`
            Dictionaries.TestSortedList();
        }

        static  void TestLists()
        {
            // Tests accessing arrays via `IList` indexer.
            Lists.ArrayListIndexers();

            Console.WriteLine();

            // Tests arrays structural equality comparison.
            Lists.ArraysEqualityComparison();
        }
    }
}
