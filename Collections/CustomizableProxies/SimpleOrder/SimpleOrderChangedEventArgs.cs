using System;
namespace Collections.CustomizableProxies.SimpleOrder
{
    /// <summary>
    /// SimpleOrderChangedEventArgs is an event argument for the Changed event.
    /// </summary>
    internal class SimpleOrderChangedEventArgs : EventArgs
    {
        internal OrderItem ChangedItem { get; }
        internal ChangeType ChangeType { get; }
        internal OrderItem ReplacedWith { get; }

        internal SimpleOrderChangedEventArgs(
            ChangeType change,
            OrderItem item, 
            OrderItem replacement)
        {
            ChangeType = change;
            ChangedItem = item;
            ReplacedWith = replacement;
        }
    }
}
