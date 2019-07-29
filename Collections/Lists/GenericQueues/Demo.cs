using System.Collections.Generic;

using static System.Console;

using static Core.ConsoleHelper;
using static Core.CollectionsHelper;

namespace Collections.Lists.GenericQueues
{
    static class Demo
    {
        /// <summary>
        /// Demonstrates usage of <see cref="Queue{T}"/>.
        /// </summary>
        internal static void Run()
        {
            Queue<int> q = new Queue<int>();

            q.Enqueue(10);
            q.Enqueue(20);

            DisplayCollectionWithSpace(q);

            int[] data = q.ToArray();
            DisplayBar();
            DisplayCollectionWithSpace(data);

            DisplayBar();
            Write(q.Count);
            DisplayBar();
            Write(q.Peek());
            DisplayBar();
            Write(q.Dequeue());
            DisplayBar();
            Write(q.Dequeue());
        }
    }
}
