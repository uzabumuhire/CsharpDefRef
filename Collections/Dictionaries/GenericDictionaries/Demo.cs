using System.Collections.Generic;

using static System.Console;

namespace Collections.Dictionaries.GenericDictionaries
{
    static class Demo
    {
        /// <summary>
        /// Demonstrates usage of <see cref="Dictionary{TKey, TValue}"/>.
        /// </summary>
        internal static void Run()
        {
            var d = new Dictionary<string, int>();

            // Specify a case-insensitive equality comparer
            // when using string keys.
            //var d = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            d.Add("One", 1);
            d["Two"] = 2;   // adds to dictionary, "Two" not already present
            d["Two"] = 22;  // updates dictionary, "Two" is now present
            d["Three"] = 3;

            WriteLine(d["Two"]);                // 22
            WriteLine(d.ContainsKey("One"));    // true (fast operation)
            WriteLine(d.ContainsValue(3));      // true (slow operation)

            int val = 0;
            if (!d.TryGetValue("onE", out val))
                WriteLine("No val");            // No val (case sensitive)

            // Three different ways to enumerate the dictionary

            foreach (KeyValuePair<string, int> kv in d)
                Write(kv.Key + "->" + kv.Value + " | ");

            WriteLine();

            foreach (string k in d.Keys)
                Write(k + "->" + d[k] + " | ");

            WriteLine();

            foreach (var v in d.Values)
                Write(v + " | ");
        }
    }
}
