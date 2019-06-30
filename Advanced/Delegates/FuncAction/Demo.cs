using static Core.CollectionsHelper;

namespace Advanced.Delegates.FuncAction
{
    static class Demo
    {
        /// <summary>
        /// Demonstrates wrtiting plug-in methods with generic delegates.
        /// </summary>
        internal static void Test()
        {
            int[] values = { 1, 2, 3 };

            // Hook in the Square method.
            Transformers.ArrayTransform(values, Square);

            DisplayCollectionWithSpace(values);
        }

        static int Square(int x) => x * x;
    }
}
