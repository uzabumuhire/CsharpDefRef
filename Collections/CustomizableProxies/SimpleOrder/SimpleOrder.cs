using System;
using System.Collections.ObjectModel;

namespace Collections.CustomizableProxies.SimpleOrder
{
    /// <summary>
    /// <para>SimpleOrder class represents a very simple keyed list of <see cref="OrderItem"/>,
    /// inheriting most of its behavior from the <see cref="KeyedCollection{TKey, TItem}"/> and 
    /// <see cref="Collection{T}"/> classes.</para>
    /// 
    /// <para>SimpleOrder is a collection of OrderItem objects, and its key
    /// is the PartNumber field of OrderItem.</para> 
    /// </summary>
    class SimpleOrder : KeyedCollection<int, OrderItem>
    {
        // The immediate base class is the constructed
        // type KeyedCollection{int, OrderItem}. When you inherit
        // from KeyedCollection, the second generic type argument is the 
        // type that you want to store in the collection -- in this case
        // OrderItem. The first type argument is the type that you want
        // to use as a key. Its values must be calculated from OrderItem; 
        // in this case it is the int field PartNumber, so SimpleOrder
        // inherits KeyedCollection<int, OrderItem>.
        
        // SimpleOrder implements a Changed event, which is raised by all the
        // protected methods.
        internal event EventHandler<SimpleOrderChangedEventArgs> Changed;

        // This parameterless constructor calls the base class constructor
        // that specifies a dictionary threshold of 0, so that the internal
        // dictionary is created as soon as an item is added to the 
        // collection.
        //
        internal SimpleOrder() : base(null, 0) { }

        // This is the only method that absolutely must be overridden,
        // because without it the `KeyedCollection` cannot extract the
        // keys from the items. The input parameter type is the 
        // second generic type argument, in this case `OrderItem`, and 
        // the return value type is the first generic type argument,
        // in this case int.
        protected override int GetKeyForItem(OrderItem item)
        {
            // In this example, the key is the part number.
            return item.PartNumber;
        }

        // SimpleOrder derives from KeyedCollection and shows how to override
        // the protected ClearItems, InsertItem, RemoveItem, and SetItem 
        // methods in order to change the behavior of the default Item 
        // property and the Add, Clear, Insert, and Remove methods. 

        // (Note that the key of OrderItem cannot be changed; if it could 
        // be changed, SimpleOrder would have to override ChangeItemKey.)

        protected override void InsertItem(int index, OrderItem item)
        {
            base.InsertItem(index, item);

            /*
            EventHandler<SimpleOrderChangedEventArgs> temp = Changed;
            if (temp != null)
            {
                temp(this, new SimpleOrderChangedEventArgs(
                    ChangeType.Added, newItem, null));
            }          
            */
            Changed?.Invoke(
                this, 
                new SimpleOrderChangedEventArgs(
                    ChangeType.Added, item, null));
        }

        protected override void SetItem(int index, OrderItem item)
        {
            OrderItem replaced = Items[index];
            base.SetItem(index, item);
            /*
            EventHandler<SimpleOrderChangedEventArgs> temp = Changed;
            if (temp != null)
            {
                temp(this, new SimpleOrderChangedEventArgs(
                    ChangeType.Replaced, replaced, newItem));
            }
            */
            Changed?.Invoke(
                this, 
                new SimpleOrderChangedEventArgs(
                    ChangeType.Replaced, replaced, item));
        }

        protected override void RemoveItem(int index)
        {
            OrderItem removedItem = Items[index];
            base.RemoveItem(index);
            /*
            EventHandler<SimpleOrderChangedEventArgs> temp = Changed;
            if (temp != null)
            {
                temp(this, new SimpleOrderChangedEventArgs(
                    ChangeType.Removed, removedItem, null));
            }
            */
            Changed?.Invoke(
                this, 
                new SimpleOrderChangedEventArgs(
                    ChangeType.Removed, removedItem, null));
        }

        protected override void ClearItems()
        {
            base.ClearItems();

            /*
            EventHandler<SimpleOrderChangedEventArgs> temp = Changed;
            if (temp != null)
            {
                temp(this, new SimpleOrderChangedEventArgs(
                    ChangeType.Cleared, null, null));
            }
            */
            Changed?.Invoke(
                this, 
                new SimpleOrderChangedEventArgs(
                    ChangeType.Cleared, null, null));
        }
    }
}
