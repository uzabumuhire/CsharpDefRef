using static Core.ConsoleHelper;

namespace Advanced.Delegates.Basics
{
    // Defines a delegate type. A delegate type defines the kind of method 
    // that delegate instances can call. Specifically, it defines the method's
    // return type and its parameter type.
    delegate int Transformer(int x);

    static class Demo
    {
        /// <summary>
        /// Demonstrates the basics concepts behind delegates.
        /// </summary>
        internal static void Test()
        {
            // Assigning a `Square` method to a `Transformer` delegate type 
            // creates a delegate instance `t`.
            // Transformer t = new Transformer(Square);
            Transformer t = Square; // creates a delegate instance

            // A delegate instance litterally acts as a delegate for the caller:
            // the caller, `Test()` in this case, invokes the delegate instance
            // `t` and then the delegate instance `t` calls the target method
            // `Square`. This indirection decouples the caller `Test()` from 
            // the target method `Square`.

            // Technically, we are specifying a method group when we refer to
            // `Square` without brackets or arguments. If the method is 
            // overloaded, C# will pick the correct overload based on the 
            // signature of the delegate to which it's being assigned.

            // A delegate is similar to a callback.

            // A delagate instance `t` is an object that knows how to call 
            // or invoke a method, in this case, the `Square`. 
            // int result = t.Invoke(3);
            int result = t(3); // Invoke delegate

            DisplaySpaceVal(result);
        }

        // The Transformer delegate type is compatible with any method 
        // with an int return type and a single int parameter.
        // static int Square(int x) {return x * x; }
        static int Square(int x) => x * x;
    }
}