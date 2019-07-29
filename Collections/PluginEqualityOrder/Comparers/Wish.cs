using System;
namespace Collections.PluginEqualityOrder.Comparers
{
    /// <summary>
    /// Describes a wish.
    /// </summary>
    class Wish
    {
        public string Name;
        public int Priority;

        public Wish(string name, int priority)
        {
            Name = name;
            Priority = priority;
        }
    }
}
