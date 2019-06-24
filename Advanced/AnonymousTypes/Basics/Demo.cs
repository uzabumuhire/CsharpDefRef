using static System.Console;

using static Core.Utility;

namespace Advanced.AnonymousTypes.Basics
{
    static class Demo
    {
        internal static void Test()
        {
            // An anonymous type is a simple class created
            // by the compiler on the fly to store a set
            // of values. To create an anonymous type, use
            // the `new` keyword followed by an object
            // initializer, specifying the properties and
            // values the type will contain. You must use
            // the `var` keyword to reference an anonymous
            // type, because it doesn't have a name.
            var dude = new { Name = "Bob", Age = 23 };
            Write(dude);
            DisplayBar();
            Write(dude.GetType());
            DisplayBar();

            // The property name of an anonymous type can be
            // inferred from an expression that is itself
            // an identifier or (ends with one)
            int Age = 23;
            var anotherDude = new { Name = "Bob", Age, Age.ToString().Length };
            //equivalenet to new { Name = "Bob", Age = Age, Length = Age.ToString().Lenght } 
            Write(anotherDude);
            DisplayBar();
            Write(anotherDude.GetType());
            DisplayBar();

            // Two ananymous type instances declared within
            // the same assembly will have the same underlying
            // type if their elements are named and typed
            // identically.
            var at1 = new { X = 2, Y = 4 };
            var at2 = new { X = 2, Y = 4 };
            Write(at1.GetType() == at2.GetType()); // True
            DisplayBar();
            Write(at1.GetType());
            DisplayBar();

            // The `Equals` method is overriden to perform
            // equality comparisons.
            Write(at1 == at2); // False
            DisplayBar();
            Write(at1.Equals(at2)); // True
            DisplayBar();

            // You can create arrays of anonymous types.
            var dudes = new[]
            {
                new { Name = "Bob", Age = 30 },
                new { Name = "Tom", Age = 40 }
            };
            Write(dude.GetType() == dudes[0].GetType()); // True
            DisplayBar();

            // A method cannot (usefully) return an anonymously typed
            // object, because it is illegal to write a method whose
            // return type is `var`.
            // var Foo() => new { Name = "Bob", Age = 30 }; // Not legal !

            // Instead, you must use `object` or `dynamic`, and whoever
            // calls `Foo`has to rely on dynamic binding, with loss of
            // static type safety (and IntelliSense)
            dynamic Foo1() => new { Name = "Bob", Age = 30 };
            Write(Foo1());
            DisplayBar();

            object Foo2() => new { Name = "Bob", Age = 30 };
            Write(Foo2());

        }
    }
}
