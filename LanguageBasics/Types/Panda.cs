namespace Basics.Types
{
    class Panda
    {
        // The `public` keyword exposes members to
        // other classes. By default, members are
        // `private`, i.e. only accessible inside
        // the class.

        // Marking a member `public` is how a type
        // communicates: "Here is what I want other
        // types to see - everything else is my own
        // implementation details." In OOP, we say
        // that the public members *encapsulate* the
        // private members of the class.

        // By default members are instance members.

        // Data member : instance field.

        // The instance field `Name` pertains to 
        // an instance of particular `Panda`
        public string Name; 

        // Data members and function members that don't
        // operate on the instance of the type, but
        // rather on the type itself, must be marked
        // as *static*.

        // A *static class* contains only static members.
        // That is all its members are static. E.g.,
        // `System.Console` is a static class and you
        // can never create instances  of a `Console`
        // one console is shared across the whole
        // application.

        // Data member : static field.

        // The static field `Population` pertains to the
        // set of all `Panda instances.
        public static int Population; 

        // Function member : construtor.
        public Panda(string n)
        {
            // Assign the instance field.
            Name = n;

            // Static field can be called inside a
            // constructor.

            // Increment the static Population field.
            Population = Population + 1;
        }
    }
}
