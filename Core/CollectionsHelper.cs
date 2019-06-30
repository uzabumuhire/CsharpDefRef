using System.Collections.Generic;

using static Core.ConsoleHelper;

namespace Core
{
    public static class CollectionsHelper
    {
        /// <summary>
        /// Displays elements of the enumerable collection with space.
        /// </summary>
        /// <param name="ec">enumerable collection</param>
        /// <typeparam name="T">type of the elements</typeparam>
        /// <exception cref="System.IO.IOException"></exception>
        /// <see cref="System.Console"/>
        public static void DisplayCollectionWithSpace<T>(IEnumerable<T> ec)
        {
            foreach (T item in ec)
            {
                DisplaySpaceVal(item);
            }
        }

        /// <summary>
        /// Displays elements of the enumerable collection without space.
        /// </summary>
        /// <param name="ec">enumerable collection</param>
        /// <typeparam name="T">type of the elements</typeparam>
        /// <exception cref="System.IO.IOException"></exception>
        /// <see cref="System.Console"/>
        public static void DisplayCollectionWithoutSpace<T>(IEnumerable<T> ec)
        {
            foreach (T item in ec)
            {
                DisplayVal(item, "");
            }
        }
    }
}
