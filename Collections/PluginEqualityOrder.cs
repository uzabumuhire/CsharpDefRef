using System;
using System.Collections;
using System.Globalization;
using System.Collections.Generic;

namespace Collections
{
    static class PluginEqualityOrder
    {
        // How an equality comparer is used to compare two customers.
        internal static void TestEqualityComparer()
        {
            // Create two customers.
            Customer c1 = new Customer("Bloggs", "Joe");
            Customer c2 = new Customer("Bloggs", "Joe");

            // Because we've not overridden `object.Equals`, normal
            // reference type equality semantics apply.
            Console.Write(c1 == c2);
            Console.Write(" | ");           // False
            Console.Write(c1.Equals(c2));       // False

            // The same default semantics apply when using these customers
            // in a `Dictionary` without specifying an equality comparer.
            var d1 = new Dictionary<Customer, string>();
            d1[c1] = "Joe";
            Console.Write(" | ");
            Console.Write(d1.ContainsKey(c2));   // False

            // Specifying the custom equality comparer.
            var lfeq = new LastFirstEqualityComparer();
            var d2 = new Dictionary<Customer, string>(lfeq);
            d2[c1] = "Joe";
            Console.Write(" | ");
            Console.Write(d2.ContainsKey(c2)); // True

            // Be careful not to change the customer's `FirstName` or
            // `LastName` while it was in use in the dictionary. Otherwise,
            // its hashcode would change and the `Dictionary` would break.
        }

        // How a comparer is used to sort a list of wishes.
        internal static void TestComparer1()
        {
            var wishList = new List<Wish>();

            wishList.Add(new Wish("Peace", 2));
            wishList.Add(new Wish("Wealth", 3));
            wishList.Add(new Wish("Love", 2));
            wishList.Add(new Wish("3 more wishes", 1));

            wishList.Sort(new PriorityComparer());

            foreach (Wish w in wishList)
                Console.Write(w.Name + " | ");
        }

        // How a comparer is used in a `SortedDictionary` to sort
        // surname strings in an order suitable for a phonebook listing
        // and using american.
        internal static void TestComparer2()
        {
            var surnames = new SortedDictionary<string, string>(
                    new SurnameComparer(new CultureInfo("en-US"))
                );

            surnames.Add("MacPhail", "second!");
            surnames.Add("MacWilliam", "third!");
            surnames.Add("McDonald", "first!");

            foreach (string v in surnames.Values)
                Console.Write(v + " | ");
        }

        // How a StringComparer is used for ordinal case-insensitive.
        internal static void TestStringComparer1()
        {
            var d = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        }

        // Sorts an array of names using Autralian English.
        internal static void TestStringComparer2()
        {
            string[] names = { "Tom", "HARRY", "sheila" };

            CultureInfo ci = new CultureInfo("en-AU");
            Array.Sort<string>(names, StringComparer.Create(ci, false));

            foreach (string n in names)
                Console.Write(n + " | ");
        }

        // Compare two arrays of integers for equality, first using 
        // the default `Equals` method, then using `IStructuralEquatable`.
        internal static void TestStructuralEquality1()
        {
            int[] a1 = { 1, 2, 3 };
            int[] a2 = { 1, 2, 3 };

            IStructuralEquatable se1 = a1;

            Console.Write(a1.Equals(a2));                                 // False
            Console.Write(" | ");
            Console.Write(se1.Equals(a2, EqualityComparer<int>.Default)); // True
        }

        // Compare two arrays of strings for equality, first using 
        // the default `Equals` method, then using `IStructuralEquatable`.
        internal static void TestStructuralEquality2()
        {
            string[] a1 = "the quick brown fox".Split();
            string[] a2 = "THE QUICK BROWN FOX".Split();

            IStructuralEquatable se1 = a1;

            Console.Write(a1.Equals(a2));                                               // False
            Console.Write(" | ");
            Console.Write(se1.Equals(a2, StringComparer.InvariantCultureIgnoreCase));   // True
        }
    }

    // Defines a customer.
    class Customer
    {
        public string LastName;
        public string FirstName;

        public Customer(string lastName, string firstName)
        {
            LastName = lastName;
            FirstName = firstName;
        }
    }

    // An equality comparer that matches both the first and the last names.
    class LastFirstEqualityComparer: EqualityComparer<Customer>
    {
        public override bool Equals(Customer x, Customer y)
            => x.LastName == y.LastName && x.FirstName == y.FirstName;

        public override int GetHashCode(Customer obj)
            => (obj.LastName + ";" + obj.FirstName).GetHashCode();
    }

    // Describes a wish.
    class Wish
    {
        public string Name;
        public int Priority;

        public Wish(string name, int priority)
        {
            Name = name;
            Priority = priority;
        }
    }

    // A comparer that sorts wishes by priority.
    class PriorityComparer : Comparer<Wish>
    {
        public override int Compare(Wish x, Wish y)
        {
            if (object.Equals(x, y))
                // `object.Equals` check ensures that we can never contradict
                // the `Equals` method. Calling the static `object.Equals` 
                // method in this case is better than calling x.Equals because
                // it still works if x is null.
                return 0; // Fail-safe check

            return x.Priority.CompareTo(y.Priority);
        }
    }

    // A culture aware comparer that sorts surname strings 
    // in an order suitable for a phonebook.
    class SurnameComparer : Comparer<string>
    {
        StringComparer strCmp;

        // Create a case-sensitive, culture-sensitive string comparer.
        public SurnameComparer(CultureInfo ci)
        {
            strCmp = StringComparer.Create(ci, false);
        }

        public override int Compare(string x, string y)
             //=> Normalize(x).CompareTo(Normalize(y));
             // Directly call `Compare` on our culture-aware `StringComparer`
             => strCmp.Compare(Normalize(x), Normalize(y));

        string Normalize(string s)
        {
            s = s.Trim().ToUpper();

            if (s.StartsWith("MC"))
                s = "MAC" + s.Substring(2);

            return s;
        }
    }
}
