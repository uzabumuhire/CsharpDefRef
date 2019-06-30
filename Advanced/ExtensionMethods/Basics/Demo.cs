using static System.Console;

using static Core.ConsoleHelper;

namespace Advanced.ExtensionMethods.Basics
{
    static class Demo
    {
        internal static void Test()
        {
            // An extension method can be called
            // as though it were an instance
            // method on a string.
            Write("Perth".IsCapitalized().ToString());

            DisplayBar();

            // Interfaces can be extended too.
            Write("Seatle".First());

        }
    }
}
