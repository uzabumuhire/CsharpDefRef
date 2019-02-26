using System;

namespace Introduction
{
    class Program
    {
        static void Main(string[] args)
        {
            // C# 7 : numerical literals with digital separators.
            int million = 1_000_000;
            Console.WriteLine(million);

            // C# 7 : binary literals specified with 0b.
            var b = 0b1010_1011_1100_1101_1110_1111;
            Console.WriteLine(b);

            // C# 7 : declaring out variables on the fly.
            bool success = int.TryParse("123", out int result);
            if(success)
            {
                Console.WriteLine(result);
            }

            // C# 7 : when calling a method with out parameters,
            // you can discard ones you're unintrested in with
            // the underscore character (_).
            DiscardOutParameters(out _, out int y, out _);
            Console.WriteLine(y);

            // Pattern variables.
            SwitchStatementPatterns(true);

            // Local methods.
            WriteCubes(11);

            // Deconstructors.
            var avery = new Person("Avery-Rose Isimbi Nefertiti Uzabumuhire");
            var (firstName, lastName) = avery; // Deconstruction
            Console.WriteLine("Avery's first and middle names is " + firstName);
            Console.WriteLine("Avery's last name is " + lastName);

            // C# 7 has explicit tuple support. Tuples provide a simple way to 
            // store a set of related values.
            var bob = ("Bob", 23);
            Console.WriteLine(bob.Item1);
            Console.WriteLine(bob.Item2);

            // C# 7 new tuples are syntatic sugar for using the 
            // System.ValueTuple<...> generic structs. Tuples elements can
            // be named.
            var tuple = (Name: "Bob", Age: "23");
            Console.WriteLine(tuple.Name);
            Console.WriteLine(tuple.Age);

            var pos = GetFilePosition();
            Console.WriteLine(pos.row);
            Console.WriteLine(pos.column);

            // Tuples implicitly support the deconstruction pattern, so they
            // can easily be deconstructed into individual variables. 
            var (row, column) = GetFilePosition(); // creates 2 local variables
            Console.WriteLine(row);
            Console.WriteLine(column);

            // C# 7: `throw` expressions
            Console.WriteLine(Capitalize("test"));

            // C# 6: null-conditional ('Elvis') operator avoids having to
            // explicitly check for null before calling a method or accessing
            // a type member.

            // s evaluates to null instead of 
            // throwing a NullReferenceException.
            System.Text.StringBuilder sb = null;
            string s = sb?.ToString(); // s is `null`

            // C# 6:  index initializers allow single-step initialization of any
            // type that exposes an indexer.
            var dict = new System.Collections.Generic.Dictionary<int, string>()
            {
                [3] = "three",
                [10] = "ten"
            };

            // C# 6: string interpolation offers an alternative 
            // to `string.Format`
            string s1 = $"It is {DateTime.Now.DayOfWeek}";

            // C# 6: exception filters let you apply a condition
            // let you a condition to a catch block.
            string html;
            try
            {
                html = new System.Net.WebClient().DownloadString("http://asef");
            }
            catch (System.Net.WebException ex) 
            when (ex.Status == System.Net.WebExceptionStatus.Timeout)
            {
                // Handle the error
            }
        }

        static void DiscardOutParameters(out int x, out int y, out int z)
        {
            x = 1;
            y = 2;
            z = 3;
        }

        static void IsOperatorPatterns (object x)
        {
            // You can introduce variables on the fly with
            // the `is` operator. These are called pattern variables.
            if (x is string s)
                Console.WriteLine(s.Length);
            else
                Console.WriteLine("It's not a string");
        }

        static void SwitchStatementPatterns (object x)
        {
            // The switch statement also supports patterns,
            // so you can switch on type as wellas constants.
            // You can also specify a `when` clause, and
            // also switch on the `null` value.
            switch (x)
            {
                case int i:
                    if (i >= 0)
                        Console.WriteLine("It's an int");
                    else
                        Console.WriteLine("It's negative");
                    break;
                case string s:
                    // we can use the s variable
                    Console.WriteLine(s.Length);
                    break;
                case bool b when b == true:
                    // matches only when b is true
                    Console.WriteLine("True");
                    break;
                case null:
                    Console.WriteLine("Nothing");
                    break;
            }
        }

        static void WriteCubes(int value)
        {
            Console.WriteLine(Cube());

            // A local method is method that can be declared inside
            // another method. Local methods are visible only to the
            // containing method, and can capture local variables 
            // in the same way that lambda expressions do.
            int Cube() => value * value;
        }

        // With tuples functions can return multiple values without
        // resorting to out parameters.
        static (int row, int column) GetFilePosition() => (3, 10);

        // In C# 7, `throw` can also appear as an expression
        // in expression-bodied function.
        static string ThrowExpression() => throw new NotImplementedException();

        // A throw expression can also appear in a ternary conditional 
        // expression.
        static string Capitalize(string value) =>
            value == null ? throw new ArgumentException("Capitalize(value)") :
            value == "" ? "" :
            char.ToUpper(value[0]) + value.Substring(1);
    }

    class Person
    {
        // C# 6 introduced the expression-bodied "fat-arrow" syntax
        // for methods, read-only properties, operators and indexers.
        // C# 7 extends this to constructors, read/write properties,
        // and finalizers.
        string name;

        public string Name
        {
            get => name;
            set => name = value ?? "";
        }

        public Person(string name) => Name = name;

        ~Person() => Console.WriteLine("finalize");

        // C# 7 introduced the deconstructor pattern. Whereas a constructor
        // typically takes a set of values (as parameters) and assigns them to
        // fields, a deconstructor dos the reverse and assigns fields back to
        // a set of variables.
        public void Deconstruct(out string firstName, out string lastName)
        {
            string[] names = name.Split(' ');

            firstName = names[0];
            for (int i = 1; i < names.Length - 1; i++)
                firstName += ' ' + names[i];

            lastName = names[names.Length - 1];
        }
    }

    class Test
    {
        // C# 6: expression-bodied functions allow methods, properties, 
        // operators, and indexers that comprise a single expression to be
        // written more tersely, in the style of lambda expression.
        public  int TimesTwo(int x) => x * 2;
        public string SomeProperty => "Property value";

        // C# 6: property intializers let you assign a value to an automatic
        // property.
        public DateTime TimeCreated { get; set; } = DateTime.Now;

        // Initialized property can also be read-only.
        public DateTime TimeCreatedReadOnly { get; } = DateTime.Now;

        // Read-only properties can be set in the constructor, making it easier
        // to create immutable (read-only) types.
    }
}
