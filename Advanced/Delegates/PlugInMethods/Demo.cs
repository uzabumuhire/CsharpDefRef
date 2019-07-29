using static Core.CollectionsHelper;

namespace Advanced.Delegates.PlugInMethods
{
    static class Demo
    {
        /// <summary>
        /// Demonstrates writing plug-in methods with delegates.
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
