using System;

using static System.Console;

using static Core.ConsoleHelper;

namespace Advanced.NullableTypes.Basics
{
    static class Demo
    {
        /// <summary>
        /// Demonstrates usage of nullable types.
        /// </summary>
        internal static void Test()
        {
            // Reference types can represent a nonexistent value with
            // a null reference. Value types, cannot ordinarily represent
            // null values.
            //string s = null; // OK, reference type can be null.
            //int i = null;// compile error, value type cannot be null.

            // To represent null in a value type, you must use a special
            // construct called a nullable type. A nullable type is denoted
            // with a value type followed by the ? symobl.
            int? i = null; // OK, nullable type
            Write(i == null); // True

            // T? translates to System.Nullable<T>, a immutable structure
            // having only two fields, to represent `Value` or `HasValue`.
            // int? i = null; translates to Nullable<int> i = new Nullable<int>();
            // Console.Write(i == null); translates to Console.Write(! i.HasValue);

            // Attempting to retrieve `Value` when `HasValue` is `false` throws
            // an InvalidOperationException.

            // `GetValueOrDefault` returns `Value` if `HasValue` is `true`,
            // otherwise returns `new T()` or a specified custom default value.

            // The default value for `T?` is `null`.

            // Implicit and explicit nullable conversions.

            // The explicit cast is equivalent to calling nullable object's `Value`
            // property. Hence, an `InvalidOperationException` is thrown if
            // `HasValue` is false.

            int? x = 5; // implicit
            int y1 = (int)x; // explicit

            DisplayBar();

            // Boxing and unboxing nullable values.

            // When T? is boxed, the boxed value on the heap contains T, not T?.
            // This optimisation is possible because a boxed value is a reference
            // type that can already express `null`.
            object o1 = x;

            // C# permits unboxing of nullable types with the `as`
            // operator. The result be will be `null` if the cast fails.
            int? x2 = o1 as int?;
            object o2 = "abc";
            x2 = o2 as int?;
            Write(x2 == null); // True

            DisplayBar();

            // Operator lifting.

            // The compiler borrows or "lifts" the operators from the underlying
            // type value. You can easily use T's operators on T?.
            int? y2 = 10;
            bool b1 = x < y2; // translates (x1.HasValue && x2.HasValue) ? (x1.Value < x2.Value) : false;
            DisplaySpaceVal(b1); // True
            DisplaySpaceVal(y2 < x); // False

            // Operator lifting means that you can implicitly use T's operators on T?.
            // You can define operators for T? in order to provide special-purpose
            // null behavior, but in the vast majority of cases, it's best to rely
            // on the compiler automatically applying systematic nullable logic for you.

            int? y = null;

            // Equality operator examples
            DisplaySpaceVal(x == y); // False
            DisplaySpaceVal(x == null); // False
            DisplaySpaceVal(x == 5); // True
            DisplaySpaceVal(y == null); // True
            DisplaySpaceVal(y == 5); // False
            DisplaySpaceVal(y != 5); // True

            // Relational operators examples
            DisplaySpaceVal(x < 6); // True
            DisplaySpaceVal(y < 6); // False
            DisplaySpaceVal(y > 6); // False

            // All other operators examples
            DisplaySpaceVal(x + 5); // 10
            DisplaySpaceVal(x + y); // null (prints empty line)

            // The compiler performs null logic differently depending on the
            // category of operator.

            // Equality operators (==, !=)

            // If exactly one operand is null, the operands are unequal.
            // If both operands are non-null, their `Values` are compared.
            // Lifted equality operators handle nulls just like reference types do.
            // This means two null values are equal.
            //DisplaySpaceVal(null == null); // True
            //DisplaySpaceVal((bool?)null == (bool?)null); // True

            // Relational operators (<, <=, >=, >)

            // These operators work on the principle that is meaningless
            // to compare null operands. This means comparing a null value to
            // either a null or non-null value returns 'false'.
            //DisplaySpaceVal((int?)null <= (int?)null); // False

            // x < y translates to (x.HasValue && y.HasValue) ? (x.Value < y.Value) : false;
            DisplaySpaceVal(x < y); // False

            DisplayBar();

            // All other operators (+, -, *, /, %, &, |, ^, <<, >>, +, ++, -, --, !, ~)
            // These operators return null when any of the operands are null (MS SQL Server).
            // An exception is when the & and | are applied to bool?.
            int? c = x + y; // (x.HasValue && y.HasValue) ? (int?)(x.Value + y.Value) : null;
            DisplaySpaceVal(c); // c is null (assuming x is 5 and y is null)

            // Mixing nullable and non-nullable operators.
            // You can mix and match nullable and non-nullable types (this is works because
            // there is an implicit conversion from T to T?)
            int? a = null;
            int b = 2;
            int? d = a + b; // d is null - equivalent to a + (int?)b.
            DisplaySpaceVal(d);

            DisplayBar();

            // bool? with & and | operators.

            // When supplied operands of type bool? the & and | operators treat `null`
            // as an unknown value. So `null | true` is true, because :
            // - if the unknown value is false, the result would be true
            // - if the unknown value is true, the result would be true
            // Similary, 'null & false' is false, because :
            // - if the unknown value is false, the result would be false
            // - if the unknown value is true, the result would be false

            bool? n = null;
            bool? t = true;
            bool? f = false;

            DisplaySpaceVal(n | n); // null
            DisplaySpaceVal(n | f); // null
            DisplaySpaceVal(n | t); // True
            DisplaySpaceVal(n & n); // null
            DisplaySpaceVal(n & f); // False
            DisplaySpaceVal(n & t); // null

            DisplayBar();

            // Nullable types and null operators.

            // Nullable types work well with the ?? operator (null coalescing operator).
            int? p = null;
            int q = p ?? 5; // q = 5, if p is not null, the expression on the left side is never evaluated
            DisplaySpaceVal(q);

            int? r = 1, s = 2;
            DisplaySpaceVal(x ?? r ?? s); // 1 (first non-null value)

            // Nullable types also work with the operator ?. (null-conditional operator). 
            System.Text.StringBuilder sb = null;
            int? length = sb?.ToString().Length;
            DisplaySpaceVal(length);

            // We can combine this with null coalescing operator ?? to evaluate
            // to zero instead of null.
            int l = sb?.ToString().Length ?? 0; // Evaluates to zero if sb is null
            DisplaySpaceVal(l);
        }
    }
}
