using static Core.CollectionsHelpers;

namespace Advanced.Delegates.PlugInMethods
{
    static class Demo
    {
        /// <summary>
        /// Demonstrates wrtiting plug-in methods with delegates.
        /// </summary>
        internal static void Test()
        {
            int[] values = { 1, 2, 3 };
            Transformers.ArrayTransform(values, Square);
            DisplayCollectionWithSpace(values);
        }

        static int Square(int x) => x * x;
    }
}
