using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using static System.Console;

using static Core.ConsoleHelper;

namespace Collections.Lists.ArrayLists
{
    static class Demo
    {
        /// <summary>
        /// Demonstrates usage of <see cref="ArrayList"/>.
        /// </summary>
        internal static void Run()
        {
            // ArrayList class requires casts.
            ArrayList al = new ArrayList();
            al.Add("hello");
            al.Add("world");
            string first = (string)al[0];
            Write(first);

            string[] strArr = (string[])al.ToArray(typeof(string));
            DisplayBar();
            Array.ForEach(strArr, DisplaySpaceVal);

            // `ArrayList` casts cannot be verified by the compiler,
            // the following compiles but fails at runtime.
            //int firstItem = (int)al[0]; 

            // Using `System.Linq.Enumerable` extension methods `Cast` and  
            // `ToList`, you  can convert an `ArrayList` to a generic `List`.
            ArrayList numbers = new ArrayList();
            numbers.AddRange(new[] { 1, 5, 9 });
            List<int> list = numbers.Cast<int>().ToList();
            DisplayBar();
            list.ForEach(DisplaySpaceVal);
        }
    }
}
