namespace Collections.CustomizableProxies.CollectionExtension
{
    /// <summary>
    /// Exposes the <see cref="AnimalCollection"/>.
    /// </summary>
    class Zoo
    {
        // The class that will expose `AnimalCollection`.
        internal readonly AnimalCollection Animals;

        internal string Name;

        internal Zoo(string name)
        {
            Animals = new AnimalCollection(this);
            Name = name;
        }
    }
}
