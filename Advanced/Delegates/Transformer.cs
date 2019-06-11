using static Core.Utility;

namespace Advanced.Delagates
{
    delegate int Transformer(int x);

    static class Demo
    {
        internal static void TransformerTest()
        {
            Transformer t = Square;
            int result = t(3);
            DisplaySpaceVal(result);
        }

        static int Square(int x) => x * x;
    }
}