using System.Collections.Generic;

using static System.Console;

namespace Collections
{

    static class CollectionsHelpers
    {
        /// <summary>
        /// Displays elements of the enumerable collection with space.
        /// </summary>
        /// <param name="ec">enumerable collection</param>
        /// <typeparam name="T">type of the elements</typeparam>
        /// <exception cref="System.IO.IOException"></exception>
        /// <see cref="System.Console"/>
        internal static void DisplayCollectionWithSpace<T>(IEnumerable<T> ec)
        {
            foreach (T item in ec)
            {
                WriteSpaceVal(item);
            }
        }

        /// <summary>
        /// Displays elements of the enumerable collection without space.
        /// </summary>
        /// <param name="ec">enumerable collection</param>
        /// <typeparam name="T">type of the elements</typeparam>
        /// <exception cref="System.IO.IOException"></exception>
        /// <see cref="System.Console"/>
        internal static void DisplayCollectionWithoutSpace<T>(IEnumerable<T> ec)
        {
            foreach (T item in ec)
            {
                WriteVal(item, "");
            }
        }

        internal static void WriteSpaceVal<T>(T val)
        {
            Write(val + " ");
        }

        internal static void WriteVal<T>(T val, string separator)
        {
            Write(val + separator);
        }

        internal static void DisplayBar()
        {
            Write(" | ");
        }
    }
   
}
