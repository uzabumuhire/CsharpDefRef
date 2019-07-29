using System.Collections;
using System.Collections.Generic;

using static System.Console;

using static Core.CollectionsHelper;

namespace Collections.Enumeration.Basics
{
    static class Demo
    {
        /// <summary>
        /// Demonstrates basic usage of enumeration.
        /// </summary>
        internal static void Run()
        {
            // Enumeration for strings as characters of arrays
            EnumerateStringLowLevel("1:Uzabumhire", " ");
            WriteLine();

            EnumerateStringHighLevel("2:Uzabumhire", " ");
            WriteLine();

            // Enumeration for arrays 
            EnumerateArraysLowLevel(new int[] { 26, 27, 28, 29, 30 });
            WriteLine();

            EnumerateArraysHighLevel(new int[] { 31, 32, 33, 34, 35 });
            WriteLine();

            // Demonstrates when to use nongeneric interfaces.
            WriteLine(
                Count(new object[]
                {
                    new int[] { 26, 27 },
                    new string[] { "aa", "bb" },
                    new char[] { 'a', 'b' }
                }));
        }

        /// <summary>
        /// Enumerates string via high-level usage of <c>foreach</c> statement.
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="data"/></typeparam>
        /// <param name="data">The array of data to enumerates.</param>
        static void EnumerateArraysHighLevel<T>(T[] data)
        {
            // You rarely write the code in `EnumerateArraysLowLevel`
            // thanks to the `foreach` statement.
            foreach (T item in data)
                Write(item + " ");
        }

        /// <summary>
        /// Enumerates string via low-level usage of <see cref="IEnumerable{T}"/>,
        /// <see cref="IEnumerator{T}"/>, <see cref="IEnumerable"/> and <see cref="IEnumerator"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="data"/></typeparam>
        /// <param name="data">The array of data to enumerates.</param>
        static void EnumerateArraysLowLevel<T>(T[] data)
        {
            // It's standard practice for collection classes to publicly expose
            // `IEnumerable<T>` while hiding the nongeneric `IEnumerable` through
            // explicit interface implementation. This is so that if you directly
            // call `GetEnumerator`, you get back the type-safe generic
            // `IEnumerator<T>`.

            // This rule is broken for reasons of backward compatibility (generics
            // did not exist prior to C# 2.0). A good example is arrays - these
            // must return the nongeneric (classic) `IEnumerator` to avoid breaking
            // earlier code.

            // In order to get a generic `IEnumerator<T>`, you must cast to expose
            // the explicit interface.

            // `IEnunmerator<T>` inherits from `IDisposable`. This allows enumerators
            // to hold references to ressources such as database connections and ensure
            // that those resources are released when enumeration is complete (or
            // abandoned pathway through). The `using` block ensures disposal.
            using (var e = ((IEnumerable<T>)data).GetEnumerator())
                while (e.MoveNext())
                    Write(e.Current + " ");
        }

        /// <summary>
        /// Enumerates string via high-level usage of <c>foreach</c> statement.
        /// </summary>
        /// <param name="s">A given string to enumerate.</param>
        /// <param name="separator">A separator used for enumeration.</param>
        static void EnumerateStringHighLevel(string s, string separator)
        {
            // It is rare to call methods on enumerators directly
            // like in `EnumerateString1`, because C# provides a syntactic
            // shortcut: the `foreach` statement
            foreach (char c in s)
                Write(c + separator);
        }

        /// <summary>
        /// Enumerates string via low-level usage of
        /// <see cref="IEnumerable"/> and <see cref="IEnumerator"/>.
        /// </summary>
        /// <param name="s">A given string to enumerate.</param>
        /// <param name="separator">A separator used for enumeration.</param>
        static void EnumerateStringLowLevel(string s, string separator)
        {
            // Because string implements `IEnumerable`
            // we can call `GetEnumerator`
            IEnumerator e = s.GetEnumerator();

            while (e.MoveNext())
            {
                char c = (char)e.Current;
                Write(c + separator);
            }
        }
    }
}
