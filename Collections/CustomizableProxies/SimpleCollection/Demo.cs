using static Core.ConsoleHelper;

namespace Collections.CustomizableProxies.SimpleCollection
{
    static class Demo
    {
        /// <summary>
        /// Demonstrates typical skeleton use
        /// of customized collection.
        /// </summary>
        internal static void Run()
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
