using static System.Console;

using static Core.ConsoleHelper;
using static Core.CollectionsHelper;

namespace Collections.CustomizableProxies.ReadOnlyProxy
{
    static class Demo
    {
        /// <summary>
        /// Demonstrates how to use a read only proxy
        /// <see cref="ReadOnlyList"/>.
        /// </summary>
        internal static void Run()
        {
            ReadOnlyList rol = new ReadOnlyList();

            Write(rol.Names.Count);
            DisplayBar();

            rol.AddInternally();
            rol.AddInternally();
            rol.AddInternally();
            DisplayCollectionWithSpace(rol.Names);

            //rol.Names.Add("Test"); // compiler error
            //((IList<string>) rol.Names).Add("test"); // NotSupportedException
        }
    }
}
