using System.Collections;
using System.Collections.Generic;

namespace Collections.Enumeration.Custom
{
    /// <summary>
    /// A basis class from which to write more
    /// sophisticated collection.
    /// </summary>
    class MyGenericCollection : IEnumerable<int>
    {
        int[] data = { 11, 12, 13, 14, 15 };

        // Because `IEnumerable<T>` inherits from `IEnumerable`,
        // we must implementboth the generic and nongeneric
        // versions of `GetEnumerator`.

        public IEnumerator<int> GetEnumerator()
        {
            foreach (var i in data)
                yield return i;
        }

        // In accordance with standard practice, we've implemented
        // the nongeneric version explicitly. Explicit implementation
        // keeps it hidden.

        IEnumerator IEnumerable.GetEnumerator()
        {
            // Simply calls the generic `GetEnumerator` because 
            // `IEnumerator` inherits from `IEnumerator<T>`.
            return GetEnumerator();
        }
    }
}
