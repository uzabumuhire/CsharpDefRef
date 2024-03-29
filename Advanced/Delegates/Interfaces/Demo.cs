﻿using static Core.ConsoleHelper;
using static Core.CollectionsHelper;

namespace Advanced.Delegates.Interfaces
{
    static class Demo
    {
        /// <summary>
        /// Demonstrates how interfaces can replace delegates.
        /// </summary>
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
