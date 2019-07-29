using static Core.ConsoleHelper;

namespace Advanced.Tuples.Basics
{
    static class Demo
    {
        /// <summary>
        /// Demonstrates usage of tuples.
        /// </summary>
        internal static void Test()
        {
            // The simplest way to create a tuple literal
            // is to list the desired values in parentheses.

            // Allow the compiler to infer the element types.
            var bob = ("Bob", 23);

            // This creates a tuple with unnamed elements,
            // which you refer to as Item1, Item2 and so on.
            DisplaySpaceVal(bob.Item1);
            DisplaySpaceVal(bob.Item2);
            DisplaySpaceVal(bob);

            DisplayBar();

            // Tuples are value types, with mutable (read/write) elements.
            var joe = bob; // joe is a *copy* of bob'
            joe.Item1 = "Joe"; // changes joe's Item1 fron Bob to Joe
            DisplaySpaceVal(bob); // (Bob, 23)
            DisplaySpaceVal(joe); // (Joe, 23)

            DisplayBar();

            // Unlike with anonymous types, you can specify a tuple type
            // explicitly. Just list each element types in parentheses.
            // `var` is not compulsory with tuples.
            // (string, int) is an alias of ValueTuple<string, int>.
            (string, int) dude = ("John", 41);
            DisplaySpaceVal(dude);

            DisplayBar();

            // You can usefully return a tuple from a method.
            (string, int) person1 = GetPerson1(); // could use `var`
            DisplaySpaceVal(person1);
            DisplaySpaceVal(person1.Item1); // Bob
            DisplaySpaceVal(person1.Item2); // 23

            DisplayBar();

            // You can optionally give meaningful names to elements
            // when creating tuple literals.
            var anotherDude = (Name: "Bob", Age: 23); // (string Name, int Age)
            DisplaySpaceVal(anotherDude);
            DisplaySpaceVal(anotherDude.Name); // Bob
            DisplaySpaceVal(anotherDude.Age); // 23

            DisplayBar();

            // You can do the same when specifying tuple types.
            var person2 = GetPerson2();
            DisplaySpaceVal(person2);
            DisplaySpaceVal(person2.Name); // Bob
            DisplaySpaceVal(person2.Age); // 23

            // Note that you can still treat the elements as unnamed and
            // refer to them as Item1, Item2, etc. (hidden by IntelliSense)
            DisplaySpaceVal(person2.Item1); // Bob
            DisplaySpaceVal(person2.Item2); // 23

            DisplayBar();

            // Tuples are type-compatible with one another if their element
            // types match up (in order). Their element names need not.
            // This leads to confusing results.
            (string Name, int Age, char Sex) bob1 = ("Bob", 23, 'M');
            (string Age, int Sex, char Name) bob2 = bob1;
            DisplaySpaceVal(bob2.Name);
            DisplaySpaceVal(bob2.Age);
            DisplaySpaceVal(bob2.Sex);

            (string Sex, int Name, char Age) bob3 = ("Bob", 41, 'M');
            DisplaySpaceVal(bob3.GetType() == bob1.GetType());
            DisplaySpaceVal(bob3.GetType());

            DisplayBar();

            // Tuples implicitly support the deconstruction pattern,
            // so you can easily deconstruct a tuple into
            // individual variables.
            (string name, int age) = bob;
            DisplaySpaceVal(name);
            DisplaySpaceVal(age);

            // We can also use type inference to deconstruct.
            var (myName, myAge, mySex) = GetBob(); // no variable name
            DisplaySpaceVal(myName);
            DisplaySpaceVal(myAge);
            DisplaySpaceVal(mySex);

            DisplayBar();

            // Equality comparison.

            // The `ValueTuple<>` types override the `Equals` method
            // to allow equality comparison to work meaningfully.
            var t1 = ("one", 1);
            var t2 = ("one", 1);
            //DisplaySpaceVal(t1 == t2); // use C# version 7.3 or greater.
            DisplaySpaceVal(t1.Equals(t2));
        }

        static (string, int) GetPerson1() => ("Bob", 23);

        static (string Name, int Age) GetPerson2() => ("Bob", 23);

        static (string, int, char) GetBob() => ("Bob", 23, 'M');

    }
}
