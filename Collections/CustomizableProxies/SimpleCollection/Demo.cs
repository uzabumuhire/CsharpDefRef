using static Core.Utility;

namespace Collections.CustomizableProxies.SimpleCollection
{
    static class Demo
    {
        // Tests typical skeleton use of `Collection<T>`.
        internal static void SimpleUsage()
        {
            Zoo zoo = new Zoo();
            zoo.Animals.Add(new Animal("Kangaroo", 10));
            zoo.Animals.Add(new Animal("Mr Sea Lion", 20));
            foreach (Animal a in zoo.Animals)
            {
                DisplaySpaceVal(a.Name);
            }
        }
    }
}
