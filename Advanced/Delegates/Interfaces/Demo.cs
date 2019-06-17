using static Core.Utility;
using static Core.CollectionsHelpers;

namespace Advanced.Delegates.Interfaces
{
    static class Demo
    {
        internal static void Test()
        {
            int[] values = { 1, 2, 3 };

            Transformers.ArrayTransform(values, new Squarer());
            DisplayCollectionWithSpace(values);

            DisplayBar();

            Transformers.ArrayTransform(values, new Cuber());
            DisplayCollectionWithSpace(values);
        }
    }
}
