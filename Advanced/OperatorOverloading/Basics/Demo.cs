using static Core.ConsoleHelper;

namespace Advanced.OperatorOverloading.Basics
{
    static class Demo
    {
        internal static void Test()
        {
            Note b = new Note(2);
            DisplaySpaceVal(b.ToString());

            Note cSharp = b + 2;
            DisplaySpaceVal(cSharp.ToString());

            // Overloading an operator automatically
            // overloads the corresponding compound
            // assignment operator.
            cSharp += 2;
            DisplaySpaceVal(cSharp.ToString());

            Note n = (Note)554.37; // explicit conversion
            DisplaySpaceVal(n.ToString());

            double x = n; // implicit conversion
            DisplaySpaceVal(x);

            // Custom conversions are ignored by
            // the `as` and `is` operators.
            //DisplaySpaceVal(554.37 is Note); // False
            //Note note = 554.37 as Note; // compiler error

            DisplayBar();

            // Demonstrate overloading `true` and `false` operators.
            // By overloading `true` and `false` operators,
            // `SQLServerBoolean` work with conditional statements
            // and operators (if, do, while, for, &&, || and ?:.
            SQLServerBoolean value = SQLServerBoolean.Null;
            if (value)
                DisplaySpaceVal("2 : " + value);
            else if (!value)
                DisplaySpaceVal("1 : " + value);
            else
                DisplaySpaceVal("0 : " + value);
        }
    }
}
