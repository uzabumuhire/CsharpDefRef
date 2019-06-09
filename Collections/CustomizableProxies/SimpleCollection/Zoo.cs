namespace Collections.CustomizableProxies.SimpleCollection
{
    class Zoo
    {
        // The class that will expose AnimalCollection.
        // This would typically have additional members.
        internal readonly AnimalCollection Animals = new AnimalCollection();
    }
}
