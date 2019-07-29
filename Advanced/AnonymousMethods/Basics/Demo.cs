using static System.Console;

namespace Advanced.AnonymousMethods.Basics
{
    static class Demo
    {
        delegate int Transformer(int i);

        /// <summary>
        /// Demonstrates usage of anonymous methods.
        /// </summary>
        internal static void Test()
        {
            // Semantically equivalent to :
            // (int x) => return x * x;
            // x => x * x;
            Transformer sqr = delegate (int x) { return x * x; };
            Write(sqr(3));
        }
    }
}
