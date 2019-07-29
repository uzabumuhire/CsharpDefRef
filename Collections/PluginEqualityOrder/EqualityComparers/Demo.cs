using System;
using System.Collections;
using System.Collections.Generic;

using static System.Console;

namespace Collections.PluginEqualityOrder.EqualityComparers
{
    static class Demo
    {
        /// <summary>
        /// Demonstrates equality comparison usage.
        /// </summary>
        internal static void Run()
        {
            // Show how `EqualityComparer` is used for equality comparer.
            CompareCustomersEquality();

            WriteLine();

            // Shows how `IStructuralEquatable` is used
            // to test equality with arrays of integers.
            IntArraysStructuralEqualityComparer();

            WriteLine();

            // Shows how `IStructuralEquatable` is used
            // to test equality with arrays of strings.
            StringArraysStructuralEqualityComparer();
        }

        /// <summary>
        /// Compare two arrays of strings for equality
        /// </summary>
        static void StringArraysStructuralEqualityComparer()
        {
            string[] a1 = "the quick brown fox".Split();
            string[] a2 = "THE QUICK BROWN FOX".Split();

            IStructuralEquatable se1 = a1;

            // Uses default `Equals` method.
            Write(a1.Equals(a2)); // False

            Write(" | ");

            // Uses `IStructuralEquatable`.
            Write(se1.Equals(a2, StringComparer.InvariantCultureIgnoreCase));   // True
        }

        /// <summary>
        /// Compare two arrays of integers for strucutural equality.
        /// </summary>
        static void IntArraysStructuralEqualityComparer()
        {
            int[] a1 = { 1, 2, 3 };
            int[] a2 = { 1, 2, 3 };

            IStructuralEquatable se1 = a1;

            // Uses the default `Equals` method.
            Write(a1.Equals(a2)); // False

            Write(" | ");

            // Uses `IStructuralEquatable`.
            Write(se1.Equals(a2, EqualityComparer<int>.Default)); // True
        }

        /// <summary>
        /// Demonstrates how an equality comparer is used to compare two customers.
        /// </summary>
        static void CompareCustomersEquality()
        {
            // Create two customers.
            Customer c1 = new Customer("Bloggs", "Joe");
            Customer c2 = new Customer("Bloggs", "Joe");

            // Because we've not overridden `object.Equals`, normal
            // reference type equality semantics apply.
            Write(c1 == c2);
            Write(" | ");           // False
            Write(c1.Equals(c2));       // False

            // The same default semantics apply when using these customers
            // in a `Dictionary` without specifying an equality comparer.
            var d1 = new Dictionary<Customer, string>();
            d1[c1] = "Joe";
            Write(" | ");
            Write(d1.ContainsKey(c2));   // False

            // Specifying the custom equality comparer.
            var lfeq = new LastFirstEqualityComparer();
            var d2 = new Dictionary<Customer, string>(lfeq);
            d2[c1] = "Joe";
            Write(" | ");
            Write(d2.ContainsKey(c2)); // True

            // Be careful not to change the customer's `FirstName` or
            // `LastName` while it was in use in the dictionary. Otherwise,
            // its hashcode would change and the `Dictionary` would break.
        }
    }
}
