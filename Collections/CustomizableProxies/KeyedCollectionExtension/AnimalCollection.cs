using System.Collections.ObjectModel;

namespace Collections.CustomizableProxies.KeyedCollectionExtension
{
    class AnimalCollection : KeyedCollection<string, Animal>
    {
        // Provides a collection of items accessible both by index and by name.
        // Provides a fast lookup by the key wich is the name of the animal.

        Zoo zoo;

        internal AnimalCollection(Zoo zoo)
        {
            this.zoo = zoo;
        }

        internal void NotifyNameChange(Animal a, string newName)
        {
            // Called if the item's key property changes,
            // in order to update the internal dictionary.
            this.ChangeItemKey(a, newName);
        }


        protected override string GetKeyForItem(Animal item)
        {
            // This is what the implementer  overrides to obtain
            // an item's key from the underlying object.
            return item.Name;
        }

        protected override void InsertItem(int index, Animal item)
        {
            base.InsertItem(index, item);
            item.Zoo = zoo;
        }

        protected override void SetItem(int index, Animal item)
        {
            base.SetItem(index, item);
            item.Zoo = zoo;
        }

        protected override void RemoveItem(int index)
        {
            this[index].Zoo = null;
            base.RemoveItem(index);
        }

        protected override void ClearItems()
        {
            foreach (Animal a in this)
            {
                a.Zoo = null;
            }
            base.ClearItems();
        }
    }
}
