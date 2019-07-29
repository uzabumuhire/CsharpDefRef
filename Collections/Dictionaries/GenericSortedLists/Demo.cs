using System.Reflection;
using System.Collections.Generic;

using static System.Console;

namespace Collections.Dictionaries.GenericSortedLists
{
    static class Demo
    {
        /// <summary>
        /// Demonstrates usage of <see cref="SortedList{TKey, TValue}"/> by
        /// using reflection to load all the methods defined in <see cref="object"/>
        /// into a sorted list keyed by name, and then enumerates their keys
        /// and values.
        /// </summary>
        internal static void Run()
        {
            // `MethodInfo` is in the `System.Reflection` namespace
            //var sl = new SortedList<string, MethodInfo>();
            // You can store multiple members of the the same key by making 
            // each value element a list `SortedList<string, List<MethodInfo>>`.
            var sl = new SortedList<string, List<MethodInfo>>();

            foreach (MethodInfo m in typeof(object).GetMethods())
                //sl[m.Name] = m;
                // We are populating the dictionary through its indexer.
                // If we instead used the `Add` method, it would throw
                // an exception because the `object` class upon which we
                // are reflecting overloads the `Equals` method, and you 
                // can't add the same key twice to a dictionary. By using 
                // the indexer, the later entry overwrites the earlier 
                // entry, preventing this exception. 
                if (!sl.ContainsKey(m.Name))
                    sl[m.Name] = new List<MethodInfo>() { m };
                else
                    sl[m.Name].Add(m);

            // Enumeration of keys
            foreach (string name in sl.Keys)
                Write(name + " | ");

            WriteLine();

            // Enumeration of values
            foreach (List<MethodInfo> l in sl.Values)
                foreach (MethodInfo m in l)
                    WriteLine(m.Name + " returns a type of " + m.ReturnType);

            // Retrieves the MethodInfo whose key is Equals and its aliases
            foreach (MethodInfo m in sl["Equals"])
                WriteLine(m);

            // Everything we've done would also work with a `SortedDictionary<,>`.
            // The following lines, which retrieve the last key and value, works 
            // only with a `SortedList<,>`.
            WriteLine(sl.Keys[sl.Count - 1]);                   // ToString
            WriteLine((sl.Values[sl.Count - 1])[0].IsVirtual);  // True
        }
    }
}
