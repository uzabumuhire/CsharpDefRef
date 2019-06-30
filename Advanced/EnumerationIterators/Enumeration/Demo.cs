using static Core.ConsoleHelper;

namespace Advanced.EnumerationIterators.Enumeration
{
    static class Demo
    {
        internal static void Test()
        {
            // A high-level way of iterating
            // through the characters in the
            // word "beer" using `foreach`
            // statement.
            foreach (var c in "beer")
                DisplaySpaceVal(c);

            DisplayBar();

            // The low-level way of iterating
            // through the characters in the
            // word "beer" without using a
            // `foreach` statement.
            using(var enumerator = "beer".GetEnumerator())
                while (enumerator.MoveNext())
                {
                    var element = enumerator.Current;
                    DisplaySpaceVal(element);

                }

            // If the enumerator implements `IDisposable`,
            // the `foreach` statement also acts as a `using`
            // statement, implicitly disposing the enumerator
            // object.
        }
    }
}
