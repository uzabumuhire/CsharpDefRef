using System.Collections.Generic;

namespace Collections.PluginEqualityOrder.EqualityComparers
{
    /// <summary>
    /// An equality comparer that matches both the first and the last names.
    /// </summary>
    class LastFirstEqualityComparer : EqualityComparer<Customer>
    {
        public override bool Equals(Customer x, Customer y)
            => x.LastName == y.LastName && x.FirstName == y.FirstName;

        public override int GetHashCode(Customer obj)
            => (obj.LastName + ";" + obj.FirstName).GetHashCode();
    }
}
