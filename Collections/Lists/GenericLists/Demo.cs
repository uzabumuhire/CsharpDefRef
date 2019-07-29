using System;
using System.Collections.Generic;

using static System.Console;

using static Core.ConsoleHelper;

namespace Collections.Lists.GenericLists
{
    static class Demo
    {
        /// <summary>
        /// Demonstrates usage of <see cref="List{T}"/>.
        /// </summary>
        internal static void Run()
        {
            List<string> words = new List<string>();

            words.Add("melon");
            words.Add("avocado");
            words.AddRange(new[] { "banana", "plum" });
            words.ForEach(DisplaySpaceVal);

            words.Insert(0, "lemon"); // Insert at start
            words.InsertRange(0, new[] { "peach", "nashi" }); // Insert at start
            DisplayBar();
            words.ForEach(DisplaySpaceVal);

            DisplayBar();
            Write(words[0]); // first element
            DisplayBar();
            Write(words[words.Count - 1]); // last element

            List<string> subset = words.GetRange(1, 2); // 2nd -> 3rd words
            DisplayBar();
            subset.ForEach(DisplaySpaceVal);

            string[] wordsArray = words.ToArray(); // Creates a new typed array
            DisplayBar();
            Array.ForEach(wordsArray, DisplaySpaceVal);

            // Copy the first two elements to the end of an existing array.
            string[] existingArray = new string[1000];
            words.CopyTo(0, existingArray, 998, 2);

            // Converting to upper case.
            List<string> upperCaseWords = words.ConvertAll(s => s.ToUpper());
            DisplayBar();
            upperCaseWords.ForEach(DisplaySpaceVal);

            // Converting to lengths.
            List<int> lengths = words.ConvertAll(s => s.Length);
            DisplayBar();
            lengths.ForEach(DisplaySpaceVal);

            words.Remove("melon");
            words.RemoveAt(3);
            words.RemoveRange(0, 2); // Removes the first 2 elements
            DisplayBar();
            words.ForEach(DisplaySpaceVal);

            // Remove all strings containing 'n'.
            words.RemoveAll(s => s.Contains("n"));
            DisplayBar();
            words.ForEach(DisplaySpaceVal);
        }
    }
}
