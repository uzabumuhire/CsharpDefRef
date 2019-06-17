using static System.Console;

using static Core.Utility;

namespace Advanced.Delegates.Compatibility
{
    static class Demo
    {
        // Type compatibility.
        delegate void D();
        delegate void D1();
        delegate void D2();
        delegate int D3(int x);

        /// <summary>
        /// Demonstrates delegates type compatibility.
        /// </summary>
        internal static void Types()
        {
            // Delegate types are incompatible with one another,
            // even if their signatures are the same.
            D1 d1 = TargetMethod;
            D2 d2 = null;
            //d2 = d1; // compile-time error
            d2 += new D2(d1); // same signature

            D3 d3 = TargetMethod;
            //d2 += new D2(d3); // compile-time error, different signature

            // Delegate instance are considered equal
            // if they have the same target method.
            D d4 = TargetMethod;
            D d5 = TargetMethod;
            D d6 = AnotherTargetMethod;
            Write(d4 == d5);
            DisplayBar();
            Write(d4 == d6);
            d4 = d6; 

            //D2 d2 = TargetMethod;
            //Write(d1 == d2); // compile-time error

        }

        static void TargetMethod() { }
        static void AnotherTargetMethod() { }
        static int TargetMethod(int x) => x;

        // Parameter compatibility.
        delegate void StringAction(string s);

        /// <summary>
        /// Demonstrates delegate's parameter compatibility. A delegate 
        /// can have more specific parameter types than its  method target. 
        /// This is called contravariance.
        /// </summary>
        internal static void Contravariance()
        {
            // A delegate merely calls a method on someone else's behalf.
            // In this case, `StringAction` is invoked with an argument
            // of type `string`. When the argument is then relayed to the
            // target method `ActOnObject`, the argument gets upcast to
            // an `object`.

            // StringAction sa = new StringAction(ActOnObject);
            StringAction sa = ActOnObject;
            sa("hello");
        }

        static void ActOnObject(object o) => Write(o);

        // Return type compatibility.
        delegate object ObjectRetriever();

        /// <summary>
        /// Demonstrates return type compatibility. A delegate's target
        /// method may return a more specific type than the type described
        /// by the delegate. This is called covariance.
        /// </summary>
        internal static void Covariance()
        {
            // `ObjectRetriever` expects to get back an `object`, 
            // but an `object` subclass will also do as delegate's 
            // return types are covariant.
            //ObjectRetriever or = new ObjectRetriever(RetrieveString);
            ObjectRetriever or = RetrieveString;
            object result = or();
            Write(result);
        }

        static string RetrieveString() => "Hello world";
    }
}
