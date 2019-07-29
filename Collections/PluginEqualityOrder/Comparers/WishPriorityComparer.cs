using System.Collections.Generic;

namespace Collections.PluginEqualityOrder.Comparers
{
    /// <summary>
    /// A comparer that sorts wishes by priority.
    /// </summary>
    class WishPriorityComparer : Comparer<Wish>
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
}
