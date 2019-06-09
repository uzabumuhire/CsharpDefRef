using System.Collections.ObjectModel;

namespace Collections.CustomizableProxies.CollectionExtension
{
    class AnimalCollection : Collection<Animal>
    {
        // `AnimalCollection2` is already a fully functioning list of animals.
        // Override each of the virtual methods in `Collection<Animal2>` 
        // to maintain the `Zoo` property in `Animal2` automatically.

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
