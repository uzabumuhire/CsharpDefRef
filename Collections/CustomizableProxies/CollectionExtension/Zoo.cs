namespace Collections.CustomizableProxies.CollectionExtension
{
    class Zoo
    {
        // The class that will expose `AnimalCollection2`.
        internal readonly AnimalCollection Animals;

        internal string Name;

        internal Zoo(string name)
        {
            Animals = new AnimalCollection(this);
            Name = name;
        }
    }
}
