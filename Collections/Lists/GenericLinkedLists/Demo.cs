using System.Collections.Generic;

using static Core.ConsoleHelper;
using static Core.CollectionsHelper;

namespace Collections.Lists.GenericLinkedLists
{
    static class Demo
    {
        /// <summary>
        /// Demonstrates usage of <see cref="LinkedList{T}"/>.
        /// </summary>
        internal static void Run()
        {
            LinkedList<string> tune = new LinkedList<string>();

            tune.AddFirst("do");
            tune.AddLast("so");
            tune.AddAfter(tune.First, "re");
            tune.AddAfter(tune.First.Next, "mi");
            tune.AddBefore(tune.Last, "fa");
            DisplayCollectionWithSpace(tune);

            tune.RemoveFirst();
            tune.RemoveLast();
            DisplayBar();
            DisplayCollectionWithSpace(tune);

            LinkedListNode<string> miNode = tune.Find("mi");
            tune.Remove(miNode);
            DisplayBar();
            DisplayCollectionWithSpace(tune);

            tune.AddFirst(miNode);
        }
    }
}
