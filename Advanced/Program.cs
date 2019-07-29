using static System.Console;

using static Core.ConsoleHelper;

namespace Advanced
{
    class Program
    {
        /// <summary>
        /// Demonstrates usage of advanced concepts in C#.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // DELEGATES
            DisplayDemo("DELEGATES", DelegatesDemo);

            // EVENTS
            DisplayDemo("EVENTS", EventsDemo);

            // LAMBDA EXPRESSIONS
            DisplayDemo("LAMBDA EXPRESSIONS", LambdaExpressionsDemo);

            // ANONYMOUS METHODS
            DisplayDemo("ANONYMOUS METHODS", AnonymousMethodsDemo);

            // ENUMERATION AND ITERATORS
            DisplayDemo("ENUMERATION AND ITERATORS", EnumerationIteratorsDemo);

            // NULLABLE TYPES
            DisplayDemo("NULLABLE TYPES", NullableTypesDemo);

            // EXTENSION METHODS
            DisplayDemo("EXTENSION METHODS", ExtensionMethodsDemo);

            // ANONYMOUS TYPES
            DisplayDemo("ANONYMOUS TYPES", AnonymousTypesDemo);

            // TUPLES
            DisplayDemo("TUPLES", TuplesDemo);

            // ATTRIBUTES
            DisplayDemo("ATTRIBUTES", AttributesDemo);

            // DYNAMIC BINDING
            DisplayDemo("DYNAMIC BINDING", DynamicBindingDemo);

            // OPERATOR OVERLOADING
            DisplayDemo("OPERATOR OVERLOADING", OperatorOverloadingDemo);

            // UNSAFE CODE AND POINTERS
            DisplayDemo("UNSAFE CODE AND POINTERS", UnsafeCodePointersDemo);
        }

        /// <summary>
        /// Demonstrates usage of delegates.
        /// </summary>
        static void DelegatesDemo()
        {
            Delegates.Basics.Demo.Test();

            WriteLine();

            Delegates.PlugInMethods.Demo.Test();

            WriteLine();

            Delegates.Multicasts.Demo.Test();

            WriteLine();

            Delegates.InstanceMethodTarget.Demo.Test();

            WriteLine();

            Delegates.GenericParameters.Demo.Test();

            WriteLine();

            Delegates.FuncAction.Demo.Test();

            WriteLine();

            Delegates.Interfaces.Demo.Test();

            WriteLine();

            Delegates.Compatibility.Demo.Types();
            DisplayBar();
            Delegates.Compatibility.Demo.Contravariance();
            DisplayBar();
            Delegates.Compatibility.Demo.Covariance();
        }

        /// <summary>
        /// Demonstrates usage of events.
        /// </summary>
        static void EventsDemo()
        {
            Events.Basics.Demo.Test();

            WriteLine();

            Events.StandardPattern.Demo.Test();

            WriteLine();

            Events.NonGenericHandler.Demo.Test();
        }

        /// <summary>
        /// Demonstrates usage of lambda expressions.
        /// </summary>
        static void LambdaExpressionsDemo()
        {
            LambdaExpressions.Basics.Demo.Test();

            WriteLine();

            LambdaExpressions.Capturing.Demo.Test();
        }

        /// <summary>
        /// Demonstrates usage of anonymous methods.
        /// </summary>
        static void AnonymousMethodsDemo()
        {
            AnonymousMethods.Basics.Demo.Test();
        }

        /// <summary>
        /// Demonstrates usage of enumeration and iterators.
        /// </summary>
        static void EnumerationIteratorsDemo()
        {
            EnumerationIterators.Enumeration.Demo.Test();

            WriteLine();

            EnumerationIterators.CollectionInitializers.Demo.Test();

            WriteLine();

            EnumerationIterators.Iterators.Demo.Test();
        }

        /// <summary>
        /// Demonstrates usage of nullable types.
        /// </summary>
        static void NullableTypesDemo()
        {
            NullableTypes.Basics.Demo.Test();
        }

        /// <summary>
        /// Demonstrates usage of extension methods.
        /// </summary>
        static void ExtensionMethodsDemo()
        {
            ExtensionMethods.Basics.Demo.Test();
        }

        /// <summary>
        /// Demonstrates usage of anonymous types.
        /// </summary>
        static void AnonymousTypesDemo()
        {
            AnonymousTypes.Basics.Demo.Test();
        }

        /// <summary>
        /// Demonstrates usage of tuples.
        /// </summary>
        static void TuplesDemo()
        {
            Tuples.Basics.Demo.Test();
        }

        /// <summary>
        /// Demonstrates usage of attributes.
        /// </summary>
        static void AttributesDemo()
        {
            Attributes.Basics.Demo.Test();

            WriteLine();

            Attributes.PropertyChangedPattern.Demo.Test();
        }

        /// <summary>
        /// Demonstrates usage of dynamic binding.
        /// </summary>
        static void DynamicBindingDemo()
        {
            DynamicBinding.Basics.Demo.Test();
        }

        /// <summary>
        /// Demonstrates usage of operator overloading.
        /// </summary>
        static void OperatorOverloadingDemo()
        {
            OperatorOverloading.Basics.Demo.Test();
        }

        /// <summary>
        /// Demonstrates usage of unsafe code and pointers.
        /// </summary>
        static void UnsafeCodePointersDemo()
        {
            UnsafeCodePointers.Basics.Demo.Test();
        }
    }
}
