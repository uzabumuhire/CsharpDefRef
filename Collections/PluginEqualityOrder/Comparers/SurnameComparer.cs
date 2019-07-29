using System;
using System.Globalization;
using System.Collections.Generic;

namespace Collections.PluginEqualityOrder.Comparers
{
    /// <summary>
    /// A culture aware comparer that sorts surname strings
    /// in an order suitable for a phonebook.
    /// </summary>
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
