using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;


namespace Collections
{
    static class Lists
    {
        // Tests accessing arrays via `IList` indexer.
        internal static void ArrayListIndexers()
        {
            object[] a1 = { "string", 123, true };
            FirstOrNull(a1);
            Console.Write(FirstOrNull(a1));        // string
            Console.Write(" | ");

            object[][] a2 = { 
                new object[]{ "string", 123, true },
                new object[]{ "string", 123, true } };
            Console.Write(FirstOrNull(a2));        // null
        }

        // Protects against accesssing a multi-dimensional array via
        // `IList` indexer.
        static object FirstOrNull(IList l)
        {
            // An `ArgumentException` is thrown if you try to access
            // a multi-dimensional array via `IList` indexer. Test for 
            // a multi-dimensional array at runtime with this expression.
            if (!(l.GetType().IsArray && l.GetType().GetArrayRank() > 1))
            {
                if (l == null || l.Count == 0)
                    return null;
                return l[0];
            }
            return null;
        }

        // Compares arrays equality structurally by using the
        // `StructuralComparisons` type.
        internal static void ArraysEqualityComparison()
        {
            object[] a1 = { "string", 123, true };
            object[] a2 = { "string", 123, true };

            // Two distinct arrays will always fail an equality test - unless
            // you use a custom equality comparer.
            Console.Write(a1 == a2);        // False
            Console.Write(" | ");
            Console.Write(a1.Equals(a2));   // False
            Console.Write(" | ");

            // Use of a custom equality comparer `StructuralComparisons`
            // to compare elements in an array.
            IStructuralEquatable se1 = a1;
            Console.Write(se1.Equals(a2,
                StructuralComparisons.StructuralEqualityComparer)); // True
        }
    }
}