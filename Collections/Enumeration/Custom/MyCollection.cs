using System.Collections;

namespace Collections.Enumeration.Custom
{
    class MyCollection : IEnumerable
    {
        int[] data = { 6, 7, 8, 9, 10 };

        // To implement `IEnumerable`/`IEnumerable<T>`, you must provide
        // an enumerator. You can do this in one of the following ways :
        // - If the class is wrapping another collection, by returning
        //   the wrapped collection's enumerator.
        // - Via an iterator using `yield return`.
        // - By instantiatinh your own `IEnumerator/IEnumerator<T>`
        //   implementation.
        // - Subclass an existing collection : Collection<T> is designed
        //   for this purpose.
        // - Use LINQ query operators.

        // Returning another collection's enumerator is just a matter of
        // `GetEnumerator` on the inner collection. However, this is viable
        // only in the simplest scenarios, where the items of the inner
        // collection are exactly what is required.

        // A more flexible approach  is to write an iterator, using C#'s
        // `yield return` statement. An *iterator* is a C# language
        // feature that assists in *writing* collections, in the same way
        // the `foreach` statement assists in *consuming* collection.

        // An iterator automatically handles the implementation of
        // `IEnumerable` and `IEnumerable<T>` or their generic versions.

        // `GetEnumerator` doesn't appear to return an enumerator at all. 
        // Upon parsing the `yield return` statement, the compiler writes
        // a hidden nested enumerator class behind the scenes, and then
        // refactors `GetEnumerator` to instantiate and return that class.

        // Iterators are powerful and simple (and are used extensively in
        // the implementation of LINQ-to-Object's standard query operators).
        public IEnumerator GetEnumerator()
        {
            foreach (int i in data)
                yield return i;
        }
    }
}
