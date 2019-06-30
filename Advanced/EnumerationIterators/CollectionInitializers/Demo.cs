using System.Collections.Generic;

using static Core.ConsoleHelper;
using static Core.CollectionsHelper;

namespace Advanced.EnumerationIterators.CollectionInitializers
{
    static class Demo
    {
        internal static void Test()
        {
            // Instantiate and populate an enumerable
            // object  in a single step.
            List<int> list1 = new List<int> { 1, 2, 3 };

            DisplayCollectionWithSpace(list1);
            DisplayBar();

            // The compiler translates this to the following :
            List<int> list2 = new List<int>();
            list2.Add(1);
            list2.Add(2);
            list2.Add(3);

            DisplayCollectionWithSpace(list2);
            DisplayBar();

            // This requires that the enumerable object implements
            // the `System.Collections.IEnumerable` interface, and
            // that it has  an `Add` method that has the approriate
            // number of parameters for the call.

            var dict1 = new Dictionary<int, string>()
            {
                {5, "five" },
                {10, "ten" }
            };

            DisplayCollectionWithSpace(dict1);
            DisplayBar();

            // This is valid for any type for which an indexer exists.
            var dict2 = new Dictionary<int, string>()
            {
                [3] = "three",
                [10] = "ten"
            };

            DisplayCollectionWithSpace(dict1);
            
        }
    }
}
