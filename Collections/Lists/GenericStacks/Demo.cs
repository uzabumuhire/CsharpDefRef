using System.Collections.Generic;

using static System.Console;

using static Core.ConsoleHelper;
using static Core.CollectionsHelper;

namespace Collections.Lists.GenericStacks
{
    static class Demo
    {
        /// <summary>
        /// Demonstrates usage of <see cref="Stack{T}"/>.
        /// </summary>
        internal static void Run()
        {
            Stack<int> s = new Stack<int>();

            s.Push(1);
            s.Push(2);
            s.Push(3);

            DisplayCollectionWithSpace(s);
            DisplayBar();
            Write(s.Count);
            DisplayBar();
            Write(s.Peek());
            DisplayBar();
            Write(s.Pop());
            DisplayBar();
            Write(s.Pop());
            DisplayBar();
            Write(s.Pop());
        }
    }
}
