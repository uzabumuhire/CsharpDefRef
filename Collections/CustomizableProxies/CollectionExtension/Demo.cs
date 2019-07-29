using static System.Console;

using static Core.ConsoleHelper;

namespace Collections.CustomizableProxies.CollectionExtension
{
    static class Demo
    {
        /// <summary>
        /// Demonstrates how to extend and customize a collection.
        /// </summary>
        internal static void Run()
        {
            Zoo zoo = new Zoo("Pairi Daiza");
            Animal kangaroo = new Animal("Kangaroo", 10);
            zoo.Animals.Add(kangaroo);
            Animal lion = new Animal("Mr Sea Lion", 20);
            zoo.Animals.Add(lion);
            foreach (Animal a in zoo.Animals)
            {
                DisplayVal(a.Name + " LIVES IN " + a.Zoo.Name, " | ");
            }
            zoo.Animals.Clear();

            Write(kangaroo.Name + " LIVES IN " + (kangaroo.Zoo?.Name ?? "nowhere"));
            DisplayBar();
            Write(lion.Name + " LIVES IN " + (lion.Zoo?.Name ?? "nowhere"));
        }
    }
}
