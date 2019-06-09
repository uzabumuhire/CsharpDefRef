namespace Collections.CustomizableProxies.CollectionExtension
{
    class Animal
    {
        internal string Name;
        internal int Popularity;

        // Add a `Zoo` property to Animal2,
        // so it can reference the Zoo2 
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
