using System;
using static Core.ConsoleHelper;

namespace Types
{
    class Program
    {
        /// <summary>
        /// Demonstrates usage of different type in C#.
        /// </summary>
        /// <param name="args">Program arguments</param>
        static void Main(string[] args)
        {
            // CLASSES
            DisplayDemo("CLASSES", ClassesDemo);

            // INHERITANCE
            DisplayDemo("INHERITANCE", InheritanceDemo);

            // OBJECT TYPE
            DisplayDemo("OBJECT TYPE", ObjectTypeDemo);

            // STRUCTS
            DisplayDemo("STRUCTS", StructsDemo);

            // ACCESS MODIFIERS
            DisplayDemo("ACCESS MODIFIERS", AccessModifiersDemo);

            // INTERFACES
            DisplayDemo("INTERFACES", InterfacesDemo);

            // ENUMS
            DisplayDemo("ENUMS", EnumsDemo);

            // NESTED TYPES
            DisplayDemo("NESTED TYPES", NestedTypesDemo);

            // GENERICS
            DisplayDemo("GENERICS", GenericsDemo);
        }

        /// <summary>
        /// Demonstrates usage of classes.
        /// </summary>
        static void ClassesDemo()
        {

        }

        /// <summary>
        /// Demonstrates usage of inheritance.
        /// </summary>
        static void InheritanceDemo()
        {

        }

        /// <summary>
        /// Demonstrates usage the <see cref="object"/> type.
        /// </summary>
        static void ObjectTypeDemo()
        {
            ObjectType.Demo.Test();
        }

        /// <summary>
        /// Demonstrates usage of the struct type.
        /// </summary>
        static void StructsDemo()
        {
            Structs.Demo.Test();
        }

        /// <summary>
        /// Demonstrates usage of access modifiers.
        /// </summary>
        static void AccessModifiersDemo()
        {

        }

        /// <summary>
        /// Demonstrates usage of interfaces.
        /// </summary>
        static void InterfacesDemo()
        {

        }

        /// <summary>
        /// Demonstrates usage of the enum type.
        /// </summary>
        static void EnumsDemo()
        {
            Enums.Demo.Test();
        }

        /// <summary>
        /// Demonstrates usage of the nested types.
        /// </summary>
        static void NestedTypesDemo()
        {

        }

        /// <summary>
        /// Demonstrates usage of generics.
        /// </summary>
        static void GenericsDemo()
        {
            
        }
    }
}
