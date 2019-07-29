namespace Collections.CustomizableProxies.CollectionExtension
{
    /// <summary>
    /// Represents an animal that is aware in which
    /// <see cref="Zoo"/> it lives.
    /// </summary>
    class Animal
    {
        internal string Name;
        internal int Popularity;

        // Add a `Zoo` property to Animal,
        // so it can reference the Zoo
        // in which it lives
        internal Zoo Zoo
        {
            get;
            set;
        }

        internal Animal(string name, int popularity)
        {
            Name = name;
            Popularity = popularity;
        }
    }
}
