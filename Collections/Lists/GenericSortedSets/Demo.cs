using System.Collections.Generic;

using static System.Console;

using static Core.ConsoleHelper;
using static Core.CollectionsHelper;

namespace Collections.Lists.GenericSortedSets
{
    static class Demo
    {
        /// <summary>
        /// Demonstrates usage of <see cref="SortedSet{T}"/>.
        /// </summary>
        internal static void Run()
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
