using System;

using static System.Console;

using static Core.Utility;

namespace Advanced.LambdaExpressions.Basics
{
    static class Demo
    {
        internal delegate int Transformer(int i);

        internal static void Test()
        {
            // `x` corresponds to the parameter `i` 
            // the expression `x * x` corresponds
            // to the return type `int`, therefore
            // being compatible with the 
            // `Transformer` delegate.
            // The expression can be a statement 
            // block `(x) => { return x * x; };`.

            // Assign a lambda expression.
            Transformer sqr = x => x * x;
            // Equivalent to the following local function :
            //int sqr(int x) => x * x;

            // Invoke a lambda expression.
            Write(sqr(3)); 

            DisplayBar();

            // Lambda expressions are used with the 
            // `Func` and `Action` delegates.
            Func<int, int> mySqr = (x) => { return x * x; };
            //int mySqr(int x) { return x * x; }
            Write(mySqr(5));

            DisplayBar();

            // A lambda expression that accepts two parameters.
            Func<string, string, int> totalLength = (s1, s2) => s1.Length + s2.Length;
            //int totalLength(string s1, string s2) => s1.Length + s2.Length;
            Write(totalLength("Jean Jacques", "Uzabumuhire"));

            // Explicitly specifying lambda parameter types.

            // The following code will fail to compile, because the compiler
            // cannot infer the type x.
            //Bar(x => Foo(x));

            // Specify x's type explicitly.
            Bar((int x) => Foo(x));
            // The other 2 ways of fixing the type inference issue
            Bar<int>(x => Foo(x)); // specify the paramerter for `Bar`
            Bar<int>(Foo); // as above, but with a method group.
        }

        static void Foo<T>(T x) { }

        static void Bar<T>(Action<T> a) { }
    }
}
