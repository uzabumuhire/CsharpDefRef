using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Collections.CustomizableProxies.ReadOnlyProxy
{
    /// <summary>
    /// Provides a readonly view to a list of strings 
    /// by using <see cref="ReadOnlyCollection{T}"/>.
    /// </summary>
    class ReadOnlyList
    {
        // This does only half the job. Although other types cannot reassign
        // the Names property, they can still call `Add`, `Remove`, or `Clear` 
        // on the list.
        //internal List<string> Names { get; private set; }
        
        List<string> names;
        internal ReadOnlyCollection<string> Names { get; private set; }

        internal ReadOnlyList()
        {
            names = new List<string>();
            Names = new ReadOnlyCollection<string>(names);
        }

        // The `ReadOnlyCollection<T>` class resolves this,
        // so that only members within this class can alter
        // the list of names.
        internal void AddInternally()
        {
            names.Add("Demo");
        }
    }
}
