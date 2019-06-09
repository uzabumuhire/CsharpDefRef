using static System.Console;

namespace Collections.CustomizableProxies.CollectionExtension
{
    static class Test
    {
        internal static void CustomUsage()
        {
            Zoo zoo = new Zoo("Pairi Daiza");
            Animal kangaroo = new Animal("Kangaroo", 10);
            zoo.Animals.Add(kangaroo);
            Animal lion = new Animal("Mr Sea Lion", 20);
            zoo.Animals.Add(lion);
            foreach (Animal a in zoo.Animals)
            {
                CollectionsHelpers.WriteVal(a.Name + " LIVES IN " + a.Zoo.Name, " | ");
            }
            zoo.Animals.Clear();

            Write(kangaroo.Name + " LIVES IN " + (kangaroo.Zoo?.Name ?? "nowhere"));
            CollectionsHelpers.DisplayBar();
            Write(lion.Name + " LIVES IN " + (lion.Zoo?.Name ?? "nowhere"));
        }
    }
}
