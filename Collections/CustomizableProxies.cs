using System.Collections.ObjectModel;

using static System.Console;

namespace Collections
{
    static class CustomizableProxies
    {
        internal static void SimpleUseOfCollection()
        {
            Zoo1 zoo = new Zoo1();
            zoo.Animals.Add(new Animal1("Kangaroo", 10));
            zoo.Animals.Add(new Animal1("Mr Sea Lion", 20));
            foreach (Animal1 a in zoo.Animals)
            {
                CollectionsHelpers.WriteSpaceVal(a.Name);
            }
        }

        internal static void ExtensionOfCollection()
        {
            Zoo2 zoo = new Zoo2("Pairi Daiza");
            Animal2 kangaroo = new Animal2("Kangaroo", 10);
            zoo.Animals.Add(kangaroo);
            Animal2 lion = new Animal2("Mr Sea Lion", 20);
            zoo.Animals.Add(lion);
            foreach (Animal2 a in zoo.Animals)
            {
                CollectionsHelpers.WriteVal(a.Name + " LIVES IN " + a.Zoo.Name, " | ");
            }
            zoo.Animals.Clear();

            Write(kangaroo.Name + " LIVES IN " + (kangaroo.Zoo?.Name ?? "nowhere"));
            CollectionsHelpers.DisplayBar();
            Write(lion.Name + " LIVES IN " + (lion.Zoo?.Name ?? "nowhere"));
        }
    }

    // Typical skeleton use of `Collection<T>`.
    class Animal1
    {
        internal string Name;
        internal int Popularity;

        internal Animal1(string name, int popularity)
        {
            Name = name;
            Popularity = popularity;
        }
    }

    class AnimalCollection1 : Collection<Animal1>
    {
        // `AnimalCollection1` is already a fully functioning list of animals.
        // No extra code is required.
    }

    class Zoo1
    {
        // The class that will expose AnimalCollection.
        // This would typically have additional members.
        internal readonly AnimalCollection1 Animals = new AnimalCollection1();
    }

    // Using `Collection<T>` as a base for extension.
    class Animal2
    {
        internal string Name;
        internal int Popularity;

        // Add a `Zoo` property to Animal2,
        // so it can reference the Zoo2 
        // in which it lives
        internal Zoo2 Zoo
        {
            get;
            set;
        }

        internal Animal2(string name, int popularity)
        {
            Name = name;
            Popularity = popularity;
        }
    }

    class AnimalCollection2 : Collection<Animal2>
    {
        // `AnimalCollection2` is already a fully functioning list of animals.
        // Override each of the virtual methods in `Collection<Animal2>` 
        // to maintain the `Zoo` property in `Animal2` automatically.

        Zoo2 zoo;
        internal AnimalCollection2(Zoo2 zoo)
        {
            this.zoo = zoo;
        }

        protected override void InsertItem(int index, Animal2 item)
        {
            base.InsertItem(index, item);
            item.Zoo = zoo;
        }

        protected override void SetItem(int index, Animal2 item)
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
            foreach (Animal2 a in this)
            {
                a.Zoo = null;
            }
            base.ClearItems();
        }
    }

    class Zoo2
    {
        // The class that will expose `AnimalCollection2`.
        internal readonly AnimalCollection2 Animals;

        internal string Name;

        internal Zoo2(string name)
        {
            Animals = new AnimalCollection2(this);
            Name = name;
        }
    }

}
