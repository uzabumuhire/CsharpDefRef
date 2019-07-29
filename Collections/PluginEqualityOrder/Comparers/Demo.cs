using System;
using System.Globalization;
using System.Collections.Generic;

using static System.Console;

namespace Collections.PluginEqualityOrder.Comparers
{
    static class Demo
    {
        /// <summary>
        /// Demonstrates order comparisons usage.
        /// </summary>
        internal static void Run()
        {
            // Shows how a `Comparer` is used to sort a `List`
            SortWishesByPriority();

            WriteLine();

            // Shows how a culture aware `Comparer` is used in `SortedDictionary`
            SortSurnamesEnUs();

            WriteLine();

            // Shows how a `StringComparer` is used in `Dictionary`.
            CompareSurnamesByOrdinalCaseInsentive();

            WriteLine();

            // Shows how  a `StringComparer` uses Autralian English.
            SortStringsEnAu();
        }

        /// <summary>
        /// Sorts an array of names using Autralian English.
        /// </summary>
        static void SortStringsEnAu()
        {
            string[] names = { "Tom", "HARRY", "sheila" };

            CultureInfo ci = new CultureInfo("en-AU");
            Array.Sort<string>(names, StringComparer.Create(ci, false));

            foreach (string n in names)
                Write(n + " | ");
        }

        /// <summary>
        /// Demonstrates how a string comparer is used for ordinal case-insensitive.
        /// </summary>
        static void CompareSurnamesByOrdinalCaseInsentive()
        {
            var d = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Demonstrates how a string comparer is used in a dictionary to sort
        /// surname strings in an order suitable for a phonebook listing
        /// and using american.
        /// </summary>
        static void SortSurnamesEnUs()
        {
            var surnames = new SortedDictionary<string, string>(
                    new SurnameComparer(new CultureInfo("en-US"))
                );

            surnames.Add("MacPhail", "second!");
            surnames.Add("MacWilliam", "third!");
            surnames.Add("McDonald", "first!");

            foreach (string v in surnames.Values)
                Write(v + " | ");
        }

        /// <summary>
        /// Demonstrates how a comparer is used to sort a list of wishes.
        /// </summary>
        static void SortWishesByPriority()
        {
            var wishList = new List<Wish>();

            wishList.Add(new Wish("Peace", 2));
            wishList.Add(new Wish("Wealth", 3));
            wishList.Add(new Wish("Love", 2));
            wishList.Add(new Wish("3 more wishes", 1));

            wishList.Sort(new WishPriorityComparer());

            foreach (Wish w in wishList)
                Write(w.Name + " | ");
        }
    }
}
