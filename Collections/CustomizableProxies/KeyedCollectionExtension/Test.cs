using static System.Console;

namespace Collections.CustomizableProxies.KeyedCollectionExtension
{
    static class Test
    {
        internal static void CustomUsage()
        {
            Zoo zoo = new Zoo("Virunga");
            Animal kangaroo = new Animal("Kangaroo", 10);
            zoo.Animals.Add(kangaroo);
            Animal lion = new Animal("Mr Sea Lion", 20);
            zoo.Animals.Add(lion);

            foreach (Animal a in zoo.Animals)
            {
                CollectionsHelpers.WriteVal(a.Name + " LIVES IN " + a.Zoo.Name, " | ");
            }

            Write(zoo.Animals[0].Popularity);
            CollectionsHelpers.DisplayBar();
            Write(zoo.Animals["Mr Sea Lion"].Popularity);
            CollectionsHelpers.DisplayBar();

            zoo.Animals["Kangaroo"].Name = "Mr Roo";

            foreach (Animal a in zoo.Animals)
            {
                CollectionsHelpers.WriteVal(a.Name + " LIVES IN " + a.Zoo.Name, " | ");
            }

            Write(zoo.Animals["Mr Roo"].Popularity);
            CollectionsHelpers.DisplayBar();

            zoo.Animals.Clear();

            Write(kangaroo.Name + " LIVES IN " + (kangaroo.Zoo?.Name ?? "nowhere"));
            CollectionsHelpers.DisplayBar();
            Write(lion.Name + " LIVES IN " + (lion.Zoo?.Name ?? "nowhere"));
        }
    }
}
