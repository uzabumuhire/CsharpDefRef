using static System.Console;

using static Core.Utility;


namespace Collections.CustomizableProxies.KeyedCollectionExtension
{
    static class Demo
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
                DisplayVal(a.Name + " LIVES IN " + a.Zoo.Name, " | ");
            }

            Write(zoo.Animals[0].Popularity);
            DisplayBar();
            Write(zoo.Animals["Mr Sea Lion"].Popularity);
            DisplayBar();

            zoo.Animals["Kangaroo"].Name = "Mr Roo";

            foreach (Animal a in zoo.Animals)
            {
                DisplayVal(a.Name + " LIVES IN " + a.Zoo.Name, " | ");
            }

            Write(zoo.Animals["Mr Roo"].Popularity);
            DisplayBar();

            zoo.Animals.Clear();

            Write(kangaroo.Name + " LIVES IN " + (kangaroo.Zoo?.Name ?? "nowhere"));
            DisplayBar();
            Write(lion.Name + " LIVES IN " + (lion.Zoo?.Name ?? "nowhere"));
        }
    }
}
