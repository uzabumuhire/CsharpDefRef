using static System.Console;

namespace Collections
{
    class Program
    {
        static void Main(string[] args)
        {
            // ENUMERATION
            WriteLine("ENUMERATION");
            WriteLine();
            TestEnumeration();

            // PLUGGING IN EQUALITY AND ORDER
            WriteLine();
            WriteLine();
            WriteLine("PLUGGING IN EQUALITY AND ORDER");
            WriteLine();
            TestPluginEqualityOrder();

            // DICTIONARIES
            WriteLine();
            WriteLine();
            WriteLine("DICTIONARIES");
            WriteLine();
            TestDictionaries();

            // LISTS
            WriteLine();
            WriteLine();
            WriteLine("LISTS");
            WriteLine();
            TestLists();

            // CUSTOMIZABLE COLLECTIONS & PROXIES
            WriteLine();
            WriteLine();
            WriteLine("CUSTOMIZABLE COLLECTIONS & PROXIES");
            WriteLine();
            TestCustomizableProxies();
        }

        static void TestEnumeration()
        {
            // Enumeration for strings as characters of arrays
            Enumeration.EnumerateString1("1:Uzabumhire", " ");
            WriteLine();
            Enumeration.EnumerateString2("2:Uzabumhire", " ");

            WriteLine();

            // Enumeration for arrays 
            Enumeration.EnumerateArrays1<int>(new int[] { 26, 27, 28, 29, 30 });
            WriteLine();
            Enumeration.EnumerateArrays2<int>(new int[] { 31, 32, 33, 34, 35 });

            WriteLine();

            // Iterator for collections
            MyCollection c = new MyCollection();
            foreach (var item in c)
                Write(item + " ");

            WriteLine();

            // Iterator for generic collections
            MyGenericCollection gc = new MyGenericCollection();
            foreach (var item in gc)
                Write(item + " ");

            WriteLine();

            foreach (var item in Enumeration.IntegerGenerator())
                Write(item + " ");

            WriteLine();

            // Custom implementation of enumerator for collections
            MyIntList il = new MyIntList();
            foreach (var item in il)
                Write(item + " ");

            WriteLine();

            // Custom implementation of enumerator for generic collections
            MyGenericIntList gil = new MyGenericIntList();
            foreach (var item in gil)
                Write(item + " ");
        }

        static void TestPluginEqualityOrder()
        {
            // Testing `EqualityComparer`
            PluginEqualityOrder.TestEqualityComparer();

            WriteLine();

            // Testing how a `Comparer` is used to sort a `List`
            PluginEqualityOrder.TestComparer1();

            WriteLine();

            // Testing how a `Comparer` is used in `SortedDictionary`
            PluginEqualityOrder.TestComparer2();

            WriteLine();

            // Testing how a `StringComparer` uses Autralian English.
            PluginEqualityOrder.TestStringComparer2();

            WriteLine();

            // Testing `IStructuralEquatable` with arrays of integers
            PluginEqualityOrder.TestStructuralEquality1();

            WriteLine();

            // Testing `IStructuralEquatable` with arrays of strings
            PluginEqualityOrder.TestStructuralEquality2();
        }

        static void TestDictionaries()
        {
            // Testing the `Dictionary<TKey, TValue>` class.
            Dictionaries.TestDictionary();

            WriteLine();

            // Testing the `SortedList<Tkey, TValue>` class.
            Dictionaries.TestSortedList();
        }

        static void TestLists()
        {
            // Testing `Array` class

            // Tests accessing arrays via `IList` indexer.
            Lists.ArrayListIndexers();

            WriteLine();

            // Tests arrays structural equality comparison.
            Lists.ArrayEqualityComparison();

            WriteLine();

            // Tests arrays creation and indexing.
            Lists.ArrayConstructionIndexing();

            WriteLine();

            // Tests arrays enumeration.
            Lists.ArrayEnumeration();

            WriteLine();

            // Tests arrays searching.
            Lists.ArraySearch();

            WriteLine();

            // Tests arrays sorting.
            Lists.ArraySort();

            WriteLine();

            // Tests arrays converting.
            Lists.ArrayConvert();

            WriteLine();

            // Testing `List<T>` class.
            Lists.GenericList();

            WriteLine();

            // Testing `ArrayList` class.
            Lists.NonGenericList();

            WriteLine();

            // Testing `LinkedList<T>` class.
            Lists.GenericLinkedList();

            WriteLine();

            // Testing `Queue<T>` class.
            Lists.GenericQueue();

            WriteLine();

            // Testing `Stack<T` class.
            Lists.GenericStack();

            WriteLine();

            // Testing `HashSet<T>`class.
            Lists.GenericHashSet();

            WriteLine();

            // Testing `SortedSet<T>` class.
            Lists.GenericSortedSet();
        }

        static void TestCustomizableProxies()
        {
            // Testing use `Collection<T>` class.
            CustomizableProxies.SimpleCollection.Test.SimpleUsage();

            WriteLine();

            // Testing extension of `Collection<T>` class.
            CustomizableProxies.CollectionExtension.Test.CustomUsage();

            WriteLine();

            // Testing extension of `KeyedCollection<TKey, TItem>` class.
            CustomizableProxies.KeyedCollectionExtension.Test.CustomUsage();
        }
    }
}
