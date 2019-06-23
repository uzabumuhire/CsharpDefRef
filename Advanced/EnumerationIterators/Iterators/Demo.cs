using System.Collections.Generic;

using static Core.Utility;

namespace Advanced.EnumerationIterators.Iterators
{
    static class Demo
    {
        internal static void Test()
        {
            // A `foreach` statement is a consumer of an enumerator.
            // An `iterator` is a producer of an enumerator.
            foreach (int fib in Fibs(6))
                DisplaySpaceVal(fib);

            DisplayBar();

            foreach (string s in Foo())
                DisplaySpaceVal(s);

            DisplayBar();

            // A `foreach` statement implicitly disposes the enumerator
            // if you break early, making this a safe way to consume
            // enumerators.
            foreach (string s in Foo(true))
                DisplaySpaceVal(s);

            DisplayBar();

            // When working with enumerators explicilty, a trap is to
            // abandon enumeration early without disposing it,
            // circumventing the `finally` block. You can avoid this
            // risk by wrapping explicit use of enumerators in a
            // `using` statement.
            using (var enumerator = Foo(true).GetEnumerator())
                while (enumerator.MoveNext())
                {
                    var s = enumerator.Current;
                    DisplaySpaceVal(s);
                }

            DisplayBar();

            try
            {
                DisplaySpaceVal(First(Foo(true)));
            }
            catch { }

            DisplayBar();

            // Prints the Fibonacci even numbers in the first 20 Fibonacci numbers.
            // Uses the composabililty of the iterator pattern.
            foreach (int fib in EvenNumbersOnly(Fibs(20)))
                DisplaySpaceVal(fib);

        }

        /// <summary>
        /// Generates the sequence of Fibonacci numbers of length <paramref name="fibCount"/>.
        /// </summary>
        /// <param name="fibCount">The length of the generated sequence of Fibonacci numbers.</param>
        /// <returns>A sequence of Fibonacci numbers.</returns>
        static IEnumerable<int> Fibs(int fibCount)
        {
            for (int i = 0, prevFib = 1, curFib = 1; i < fibCount; i++)
            {
                // Whereas a `return` statement expresses
                // "Here's the value you asked me to return from this method,"
                // a `yield return` statement expresses
                // "Here's the next element you asked to yield  from this enumerator."
                // On each `yield return` statement, control is returned to the caller,
                // but the callee's state is maintained so that the method can continue
                // executing as soon as the caller enumerates the next element.
                // Lifetime of this state is bound to the enumerator, such that the
                // state can be released when the caller has finished enumerating.
                yield return prevFib;

                int newFib = prevFib + curFib;
                prevFib = curFib;
                curFib = newFib;
            }
        }

        static IEnumerable<string> Foo()
        {
            // Multiple yield statements are permitted.
            yield return "One";
            yield return "Two";
            yield return "Three";
        }

        static IEnumerable<string> Foo(bool breakEarly)
        {
            // Introduce error for testing.
            //if (breakEarly)
            //    yield break;

            yield return "One";
            yield return "Two";

            // The `yield break` statement indicates that
            // the iterator block should exit early,
            // without returning more elements.
            if (breakEarly)
                yield break;

            yield return "Three";
        }

        /// <summary>
        /// Returns the first element of the <paramref name="sequence"/> or throws
        /// a null reference exception if not found.
        /// </summary>
        /// <typeparam name="T">The type of the elements in a <paramref name="sequence"/></typeparam>
        /// <param name="sequence">An iterable sequence.</param>
        /// <returns>The first element of iterable <paramref name="sequence"/></returns>
        static T First<T>(IEnumerable<T> sequence)
        {
            // Returning the first element safely.
            using (var enumerator = sequence.GetEnumerator())
                if (enumerator.MoveNext())
                    return enumerator.Current;

            throw null;
        }

        /// <summary>
        /// Output a sequence of even numbers from the input <paramref name="sequence"/>.
        /// </summary>
        /// <param name="sequence">The input sequence of integers</param>
        /// <returns>A sequence of even numbersfrom the input <paramref name="sequence"/>.</returns>
        static IEnumerable<int> EvenNumbersOnly(IEnumerable<int> sequence)
        {
            foreach (int x in sequence)
                if ((x % 2) == 0)
                    yield return x;
        }
    }
}
