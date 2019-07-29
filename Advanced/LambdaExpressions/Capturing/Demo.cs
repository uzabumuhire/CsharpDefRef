using System;

using static System.Console;

using static Core.ConsoleHelper;

namespace Advanced.LambdaExpressions.Capturing
{
    static class Demo
    {
        /// <summary>
        /// Demonstrates closure or capturing outer variables with lambda expressions.
        /// </summary>
        internal static void Test()
        {
            // Closure : capturing outer variables.

            // A lambda expression can reference the local variables  and 
            // parameters of the method in which it's defined (outer variables).
            int factor = 2;
            Func<int, int> doubler = n => n * factor;
            Write(doubler(3731));

            DisplayBar();

            // Outer variables referenced by a lambda expression are called
            // captured variables.  A lambda expression that captures variables
            // is called a closure.

            // Captured variables are evaluated when the delegate is actually
            // invoked, not when the the variables were captured.
            int f = 3;
            Func<int, int> multiplier = n => n * f;

            f = 10;
            Write(multiplier(3));

            DisplayBar();

            // Lambda expressions can themselves update captured variables.
            int seed = 0;
            Func<int> natural = () => seed++;
            DisplaySpaceVal(natural());
            DisplaySpaceVal(natural());
            Write(seed);

            DisplayBar();

            // Captured variables have their lifetimes extended to that of
            // the delegate. The local variable `seed` would ordinarily
            // disappear from scope when `NaturalNumber` finished executing;
            // But because `seed` has been captured, its lifetime is extended
            // to that of of the capturing delegate, `naturalNumber`
            Func<int> naturalNumber = NaturalNumber();
            DisplaySpaceVal(naturalNumber());
            DisplaySpaceVal(naturalNumber());

            DisplayBar();

            // A local variable instantiated within a lambda expression 
            // is unique per invocation of the delegate instance.
            Func<int> wrongNatural = WrongNatural();
            DisplaySpaceVal(wrongNatural());
            DisplaySpaceVal(wrongNatural());

            DisplayBar();

            // Capturing iteration variables.

            // When you capture the iteration variable of a `for` loop,
            // C# treats that variable as though it was declared outside
            // the loop. This means the same variable value `3` is 
            // captured in each iteration.
            Action[] actions = new Action[3];

            // Each closure `() => DisplaySpaceVal(i)` captures the same
            // variable, i.
            for (int i = 0; i < actions.Length; i++)
                actions[i] = () => DisplaySpaceVal(i);

            // When the delegates are later invokedm each delegate
            // sees i's value at the time of invocation, which is 3.
            foreach (Action a in actions)
                a();

            DisplayBar();

            // The solution is to assign the iteration variable to 
            // a local variable that's scoped inside the loop.
            Action[] myActions = new Action[3];
            for (int i = 0; i < myActions.Length; i++)
            {
                // Because loopScopedi is freshly created on every
                // iteration, each closure captures a different 
                // variable.
                int loopScopedi = i;
                myActions[i] = () => DisplaySpaceVal(loopScopedi);
            }
            foreach (Action a in myActions)
                a();

            DisplayBar();

            // Prior C# 5.0, foreach loops worked in the same way as the for
            // loop. But this was fixed, since the iteration variable in a
            // foreach loop is immutable and should be treated as local to
            // the loop body.
            Action[] myNewActions = new Action[3];
            int j = 0;
            foreach (char c in "abc")
                myNewActions[j++] = () => DisplaySpaceVal(c);
            foreach (Action a in myNewActions)
                a(); // prints "c c c" in C# 4.0 & "a b c" in C# 5.0 and laterß
        }

        static Func<int> NaturalNumber()
        {
            int seed = 0;
            return () => seed++; // returns a closure that captured the seed
        }

        static Func<int> WrongNatural()
        {
            return () => { int seed = 0; return seed++; };
        }
    }
}
