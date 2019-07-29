using System.Collections.ObjectModel;

namespace Collections.CustomizableProxies.CollectionExtension
{
    /// <summary>
    /// Uses a customizable wrapper <see cref="Collection{T}"/> for lists
    /// to provide custom behavior when animal are added to or remove
    /// from the <see cref="Zoo"/>.
    /// </summary>
    class AnimalCollection : Collection<Animal>
    {
        // `AnimalCollection` is already a fully functioning list of animals.
        // Override each of the virtual methods in `Collection<Animal>` 
        // to maintain the `Zoo` property in `Animal` automatically.

        Zoo zoo;

        internal AnimalCollection(Zoo zoo)
        {
            this.zoo = zoo;
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
