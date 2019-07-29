using System.Collections.Generic;

using static System.Console;

using static Core.ConsoleHelper;
using static Core.CollectionsHelper;

namespace Collections.Lists.GenericHashSets
{
    static class Demo
    {
        /// <summary>
        /// Demonstrates usage of <see cref="HashSet{T}"/>.
        /// </summary>
        internal static void Run()
        {
            // Constructs a `HashSet<char>` from an existing collection.
            // The reason we pass `string` into `HashSet<char>` is because
            // implements `IEnumerable<char>`.
            HashSet<char> letters = new HashSet<char>("the quick brown fox");

            // Enumerate the collection (notice the abscence of duplicates).
            DisplayCollectionWithoutSpace(letters);

            // Tests membership.
            DisplayBar();
            Write(letters.Contains('t'));
            DisplayBar();
            Write(letters.Contains('j'));

            // Extracts all the vowels from our set of characters.
            HashSet<char> lettersSet1 = new HashSet<char>(letters);
            lettersSet1.IntersectWith("aeiou");
            DisplayBar();
            DisplayCollectionWithoutSpace(lettersSet1);

            // Strips all the vowels from the set.
            HashSet<char> lettersSet2 = new HashSet<char>(letters);
            lettersSet2.ExceptWith("aeiou");
            DisplayBar();
            DisplayCollectionWithoutSpace(lettersSet2);

            // Removes all but the elements that are unique to one set or the
            // other. 
            HashSet<char> lettersSet3 = new HashSet<char>(letters);
            lettersSet3.SymmetricExceptWith("the lazy brown fox");
            DisplayBar();
            DisplayCollectionWithoutSpace(lettersSet3);
        }
    }
}
