namespace Collections.CustomizableProxies.KeyedCollectionExtension
{
    class Animal
    {
        string name;
        internal string Name
        {
            get { return name; }
            set
            {
                if (Zoo != null) 
                    Zoo.Animals.NotifyNameChange(this, value);

                name = value;
            }
        }

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
