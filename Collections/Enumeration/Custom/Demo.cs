using System.Collections.Generic;

using static System.Console;

namespace Collections.Enumeration.Custom
{
    static class Demo
    {
        /// <summary>
        /// Demonstrates custom implementation
        /// of the enumeration interfaces.
        /// </summary>
        internal static void Run()
        {
            // Shows custom `IEnumerable` implementation.
            MyCollection c = new MyCollection();
            foreach (var item in c)
                Write(item + " ");

            WriteLine();

            // Shows custom `IEnumerable<T>` implementation.
            MyGenericCollection gc = new MyGenericCollection();
            foreach (var item in gc)
                Write(item + " ");

            WriteLine();

            // Shows custom `IEnumerable<T>` implementation
            // without a class but via the `yield return`
            // statement.
            foreach (var i in HighLevelIntegerGenerator())
                Write(i + " ");

            WriteLine();

            // Shows custom implementation of enumerator
            // for collections.
            MyIntList il = new MyIntList();
            foreach (var item in il)
                Write(item + " ");

            WriteLine();

            // Shows custom implementation of enumerator
            // for generic collections.
            MyGenericIntList gil = new MyGenericIntList();
            foreach (var item in gil)
                Write(item + " ");
        }

        /// <summary>
        /// Demonstrates custom <see cref="IEnumerable{T}"/> implementation
        /// without a class but via the `yield return` statement.
        /// </summary>
        /// <returns>An enumerable collection.</returns>
        static IEnumerable<int> HighLevelIntegerGenerator()
        {
            // If we need nothing above a simple `IEnumerator<T>` implementation,
            // the `yield return` statement allows for an easier variation.
            // Rather than writing a class, you can move the iteration logic
            // into a method returning a generic `IEnumerable<T>` and let the
            // compiler take care of the rest.
            yield return 1;
            yield return 2;
            yield return 3;
            yield return 4;
            yield return 5;
        }
    }
}
