using System.Collections.Generic;

using Microsoft.CSharp.RuntimeBinder;

using static System.Console;

using static Core.Utility;

namespace Advanced.DynamicBinding.Basics
{
    static class Demo
    {
        internal static void Test()
        {
            // Custom dynamic binding : Duck implements IDMOP
            // `IDynamicMetaObjectProvider`. Duck doesn't have
            // a `Quack` or `Waddle` method. It uses custom
            // binding to intercept and interpret all method
            // calls.
            dynamic d = new Duck();
            d.Quack(); // Quack method was called
            d.Waddle(); // Waddle method was called.

            // Language dynamic binding : no implementation of IDMOP.
            // You don't have to duplicate code for each numeric type.
            // You loose static type safety, risking runtime exceptions
            // rather than compile-time errors.
            double x = 3, y = 4;
            DisplaySpaceVal(Mean(x, y));

            WriteLine();

            // RuntimeBinderException.
            dynamic a = 5;
            try
            {
                // If a member fails to bind,
                // a `RuntimeBinderException` is thrown.
                a.Hello();
            }
            catch (RuntimeBinderException ex)
            {
                DisplayError(ex.ToString());
            }

            // Runtime representation of dynamic.

            // A deep equivalence between the `dynamic` and `object` types. 
            //DisplaySpaceVal(typeof(dynamic) == typeof(object)); // ERROR
            DisplaySpaceVal(typeof(List<dynamic>) == typeof(List<object>));
            DisplaySpaceVal(typeof(dynamic[]) == typeof(object[]));

            // Like an object reference, a dynamic reference can point to
            // an object of any type (except pointer types).
            dynamic b = "Hello";
            DisplaySpaceVal(b.GetType().Name);
            b = 123; // No error (despite same variable)
            DisplaySpaceVal(b.GetType().Name);

            // There is no difference between an object reference and a
            // dynamic reference. A dynamic reference simply enables
            // dynamic operations on the object it points to. You can
            // convert  from `object` to `dynamic` to perform any
            // dynamic operation you want on an `object`.
            object o = new System.Text.StringBuilder();
            dynamic c = o;
            c.Append("Hello");
            DisplaySpaceVal(c);

            WriteLine();

            // Dynamic conversions.
            // The `dynamic` type has implicit conversions to and from
            // all other types. For the conversion to succeed, the
            // runtime type of the dynamic object must be implicitly
            // convertible to the target static type.
            int i = 7;
            dynamic e = i;
            long j = e; // No cast required (implicit conversion)
            try
            {
                short k = e; // RuntimeBinderException
            }
            catch (RuntimeBinderException ex)
            {
                DisplayError(ex.ToString());
            }

            // var vs dynamic
            // var says : let the compiler figure out the type
            // dynamic
            dynamic p = "hello"; // static type is dynamic, runtime type is string.
            //var q = "hello"; // static type is string, runtime type is string.
            int s;
            try
            {
                s = p; // runtime error (cannot convert string to int)
            }
            catch (RuntimeBinderException ex)
            {
                DisplayError(ex.ToString());
            }
           
            //int t = q; // compile-time error (cannot convert string to int)

            // The static type of a variable declared with `var` can be `dynamic`.
            dynamic n = "hello";
            var m = n; // static type of y is dynamic
            try
            {
                s = m; // runtime error (cannot convert string to int)
            }
            catch (RuntimeBinderException ex)
            {
                DisplayError(ex.ToString());
            }

            // Dynamic expressions.
            // Trying to consume the result of a dynamic expression with a `void`
            // return type is prohibited - just as with statiscally typed expression.
            // The difference is that the error occurs at runtime.
            dynamic list = new List<int>();
            try
            {
                var result = list.Add(5); // RuntimeBinderException
            }
            catch (RuntimeBinderException ex)
            {
                DisplayError(ex.ToString());
            }

            // Expressions involving dynamic operands are typically themselves
            // dyanamic, since the effect of absent type information is cascading.
            dynamic u = 2;
            var v = u * 3; // static type is dynamic

            // Casting a dynamic expression to a static
            // type yields a static expression.
            dynamic f = 2;
            var g = (int)f; // static type of y is int

            // constructor invocations always yield static expressions
            // - even when called with dynamic arguments.
            // `sb` is statically  typed to a `StringBuilder`.
            dynamic capacity = 10;
            var sb = new System.Text.StringBuilder(capacity);

            // Dynamic calls without dynamic receivers.
            u = 5;
            f = "watermelon";

            // The particular Foo that gets dynamically bound is dependent
            // on the runtime type of the dynamic argument.
            Foo(u);
            Foo(f);

            // Because a dynamic receiver is not involvedm the compiler can
            // statically perform basic check oto whether the dynamic call
            // will succeed. It checks that a function with the right name
            // and number of parameters exists. If no candidate is found,
            // you get compile-time error.
            //Foo(u, u, u); // compiler error - wrong number of parameters
            //Fook(u); // compiler error - no such method name

            DisplayBar();

            // Static types in dynamic expressions.
            // The call to Foo(o, d) is dynamically bound because one
            // its arguments, `d`, is `dyanamic`. But since `o` is statically
            // known, the binding - even though it occurs dynamically - will
            // make use of that. In this casem overload resolution will pick
            // the implementation of Foo following the static type of o and
            // the runtime type of d. In other words, the compiler is "as static
            // as it can possibly be."
            o = "hello"; // o is of static type `object`
            d = "goodbye"; // d is of static type `dynamic`, runtime type `string`I
            Foo(o, d); // Foo(object x, string y)

            WriteLine();

            // Uncallable functions.

            // Static typing. To call the `UF()` method, we must cast to `IUncallableFunctions`.

            IUncallableFunctions iuf = new UncallableFunctions(); // implicit cast to interface
            iuf.UF();

            //UncallableFunctions uf = new UncallableFunctions(); // no cast
            //uf.UF(); // compiler error (uf doesn't contain UF() method)

            // Dynamic typing. The implicit cast on line 185, tells the *compiler* to bind
            // subsequent member calls on `iuf` to `IUncallableFunctions` rather than
            // `UncallableFunctions`. In other words, to view that object (`iufÌ) through
            // the lens of the `IUncallableFucntions` interface. However, that lens is lost
            // at runtime, so the DLR (Dynamic Language Runtime) cannot complete the binding.
            dynamic duf = iuf;
            try
            {
                // RuntimeBinderException thrown :
                // `UncallabeFunctions` doesn't contain a definition for UF().
                duf.UF();
            }
            catch (RuntimeBinderException ex)
            {
                DisplayError(ex.ToString());
            }

            DisplaySpaceVal(iuf.GetType().ToString());
            
        }

        static dynamic Mean(dynamic x, dynamic y) => (x + y) / 2;

        static void Foo(int x) => DisplaySpaceVal("Foo(int x)");

        static void Foo(string x) => DisplaySpaceVal("Foo(string x)");

        static void Foo(object x, object y) => DisplaySpaceVal("Foo(object x, object y)");

        static void Foo(object x, string y) => DisplaySpaceVal("Foo(object x, string y)");

        static void Foo(string x, object y) => DisplaySpaceVal("Foo(string x, object y)");

        static void Foo(string x, string y) => DisplaySpaceVal("Foo(string x, string y)");
    }
}
