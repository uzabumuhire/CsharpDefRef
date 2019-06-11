using static System.Console;

using static Core.Utility;
using static Core.CollectionsHelpers;

namespace Collections.CustomizableProxies.ReadOnlyProxy
{
    static class Demo
    {
        internal static void ReadOnlyView()
        {
            ReadOnlyList rol = new ReadOnlyList();

            Write(rol.Names.Count);
            DisplayBar();

            rol.AddInternally();
            rol.AddInternally();
            rol.AddInternally();
            DisplayCollectionWithSpace(rol.Names);

            //rol.Names.Add("Test"); // complite error
            //((IList<string>) rol.Names).Add("test"); // NotSupportedException
        }
    }
}
