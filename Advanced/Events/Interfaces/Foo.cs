using System;
namespace Advanced.Events.Interfaces
{
    internal class Foo : IFoo
    {
        EventHandler ev;
        event EventHandler IFoo.Ev
        {
            // The add and remove parts of an event
            // are compiled to add_XXX and remove_XXX
            // methods.
            add
            {
                ev += value;
            }

            remove
            {
                ev -= value;
            }
        }
    }
}
