﻿using static System.Console;

using static Core.ConsoleHelper;

namespace Advanced
{
    class Program
    {
        static void Main(string[] args)
        {
            // DELEGATES
            WriteLine("DELEGATES");
            WriteLine();
            DelegatesDemo();

            // EVENTS
            WriteLine();
            WriteLine();
            WriteLine("EVENTS");
            WriteLine();
            EventsDemo();

            // LAMBDA EXPRESSIONS
            WriteLine();
            WriteLine();
            WriteLine("LAMBDA EXPRESSIONS");
            WriteLine();
            LambdaExpressionsDemo();

            // ANONYMOUS METHODS
            WriteLine();
            WriteLine();
            WriteLine("ANONYMOUS METHODS");
            WriteLine();
            AnonymousMethodsDemo();

            // ENUMERATION AND ITERATORS
            WriteLine();
            WriteLine();
            WriteLine("ENUMERATION AND ITERATORS");
            WriteLine();
            EnumerationIteratorsDemo();

            // NULLABLE TYPES
            WriteLine();
            WriteLine();
            WriteLine("NULLABLE TYPES");
            WriteLine();
            NullableTypesDemo();

            // EXTENSION METHODS
            WriteLine();
            WriteLine();
            WriteLine("EXTENSION METHODS");
            WriteLine();
            ExtensionMethodsDemo();

            // ANONYMOUS TYPES
            WriteLine();
            WriteLine();
            WriteLine("ANONYMOUS TYPES");
            WriteLine();
            AnonymousTypesDemo();

            // TUPLES
            WriteLine();
            WriteLine();
            WriteLine("TUPLES");
            WriteLine();
            TuplesDemo();

            // ATTRIBUTES
            WriteLine();
            WriteLine();
            WriteLine("ATTRIBUTES");
            WriteLine();
            AttributesDemo();

            // DYNAMIC BINDING
            WriteLine();
            WriteLine();
            WriteLine("DYNAMIC BINDING");
            WriteLine();
            DynamicBindingDemo();

            // OPERATOR OVERLOADING
            WriteLine();
            WriteLine();
            WriteLine("OPERATOR OVERLOADING");
            WriteLine();
            OperatorOverloadingDemo();

            // UNSAFE CODE AND POINTERS
            WriteLine();
            WriteLine();
            WriteLine("UNSAFE CODE AND POINTERS");
            WriteLine();
            UnsafeCodePointersDemo();
        }

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

        static void EventsDemo()
        {
            Events.Basics.Demo.Test();

            WriteLine();

            Events.StandardPattern.Demo.Test();

            WriteLine();

            Events.NonGenericHandler.Demo.Test();
        }

        static void LambdaExpressionsDemo()
        {
            LambdaExpressions.Basics.Demo.Test();

            WriteLine();

            LambdaExpressions.Capturing.Demo.Test();
        }

        static void AnonymousMethodsDemo()
        {
            AnonymousMethods.Basics.Demo.Test();
        }

        static void EnumerationIteratorsDemo()
        {
            EnumerationIterators.Enumeration.Demo.Test();

            WriteLine();

            EnumerationIterators.CollectionInitializers.Demo.Test();

            WriteLine();

            EnumerationIterators.Iterators.Demo.Test();
        }

        static void NullableTypesDemo()
        {
            NullableTypes.Basics.Demo.Test();
        }

        static void ExtensionMethodsDemo()
        {
            ExtensionMethods.Basics.Demo.Test();
        }

        static void AnonymousTypesDemo()
        {
            AnonymousTypes.Basics.Demo.Test();
        }

        static void TuplesDemo()
        {
            Tuples.Basics.Demo.Test();
        }

        static void AttributesDemo()
        {
            Attributes.Basics.Demo.Test();

            WriteLine();

            Attributes.PropertyChangedPattern.Demo.Test();
        }

        static void DynamicBindingDemo()
        {
            DynamicBinding.Basics.Demo.Test();
        }

        static void OperatorOverloadingDemo()
        {
            OperatorOverloading.Basics.Demo.Test();
        }

        static void UnsafeCodePointersDemo()
        {
            UnsafeCodePointers.Basics.Demo.Test();
        }
    }
}
