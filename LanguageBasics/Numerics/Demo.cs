using static System.Console;

using static Core.ConsoleHelper;
using static Core.MatrixHelper;

namespace Basics.Numerics
{
    static class Demo
    {
        /// <summary>
        /// Demonstrates basic usage of numeric types.
        /// </summary>
        internal static void Run()
        {
            Fundamentals();

            WriteLine();

            Conversions();
        }

        /// <summary>
        /// Demonstrates basic usage of numeric conversions.
        /// </summary>
        static void Conversions()
        {
            // Converting between integral types.

            // Integral type conversions are *implicit* when the
            // destination type can represent every possible
            // value of the source type. Otherwise, *explicit*
            // conversion is required.

            int x = 12345678;   // `int` is a 32-bit integer

            long y = x;         // implicit conversion to 64-bit integral type
            DisplayBarVal($"int ({x}) to long ({y})");

            short z = (short)x; // explicit conversion to 16-bit integral type
            DisplayBarVal($"int ({x}) to short ({z})"); // loss of infos

            // Converting between floating-point types.

            // A `float` can be implicitly converted to a `double`,
            // since a `double` can represent every possible value
            // of a `float`. The reverse converion must be explicit.


            // Converting between floating-point and integral types.

            // All integral types may be implicitly converted to all
            // floating-point types.

            int i1 = 1;
            float f1 = i1; // implicit conversion

            // The reverse conversion must be explicit.

            int i2 = (int)f1; // explicit conversion

            // When you cast from a floating-point type to an integeral
            // type, any fractional portion is truncated; no rounding 
            // is performed. The static class System.Convert provides
            // methods that round while converting between various
            // numeric types.

            // Implicitly converting a large integral type to a floating-
            // point type preserves *magnititude* (how big of range the
            // data type can fit) but may occasionaly lose *precision*
            // (how much a number an be can be representd by a data type).
            // This is because floating-point types always have more
            // magnititude than integral types, but may have less
            // precision.

            int i3 = 100_000_001;
            float f2 = i3;          // magnitude preserved, precision lost
            DisplayBarVal($"int ({i3}) to float ({f2})");

            int i4 = (int)f2;       // 100_000_000
            DisplayBarVal($"float ({f2}) back int ({i4})");

            // Decimal conversions.

            // All integral types can be implicitly converted to the
            // decimal type, since a decimal represent every possible
            // C# integral-type value. All other numeric conversion to
            // and from a decimal tye must be explicit.
        }

        /// <summary>
        /// Demonstrates fundementals of numeric types.
        /// </summary>
        static void Fundamentals()
        {
            // Predefined numeric values types in C# :
            // - Integral
            //  * Signed integer (`sbyte`, `short`, `int`, `long`)
            //  * Unsigned integer (`byte`, `ushort`, `uint`, `ulong`)
            // - Real
            //  * Real number (`float`, `double`, `decimal`)

            // Of the *integral* number types, `int` and `long` are
            // first-class citizens and are favored by both
            // C# and runtime. The other integral types are
            // typically used for interoperability or when
            // space efficiency is paramount.

            // Of the *real* number types, `float` and `double` are
            // called *floating-point types* and are typically used
            // for scientific and graphical calculations. The
            // `decimal` type is typically used for financial
            // calculations where base-10-accurate arthmetic and
            // high precision are required. Technically, `decimal`
            // is a floating-point type too, although it's not
            // referred to as such in the C# language specification.

            // Numeric literals

            // *Literals* are primitive pieces of data lexically
            // embedded into the program.

            // *Integral-type literals* can use decimal or hexadecimal
            // (prefixed with 0x prefix) notation.

            int x = 127;
            DisplayBarVal(x);

            long y = 0x7F; // or with lower-case letters 0x7f
            DisplayBarVal(y);

            long[] l = { 0x0, 0x10, 0x100, 0x1000, 0x10_000, 0x100_000, 0x1_000_000 };
            Display1DMatrix(l);
            DisplayBar();

            // C# 7, you can insert an underscore anywhere inside
            // the numeric literal to make it more readable.
            int million1 = 1_000_000;
            DisplayBarVal(million1);

            // C# 7, lets you specify numbers in binary with 0b prefix.
            var b = 0b1010_1011_1100_1101_1110_1111;
            DisplayBarVal(b);

            // *Real-type literals* use decimal and/or exponential notation.
            double d = 1.5;
            DisplayBarVal(d);

            double million2 = 1E06; // or with lower-case letters 1e06
            DisplayBarVal(million2);

            // Numeric literal type inference

            // By default, the compiler *infer* a numeric literal to be
            // either `double` or an integral type.
            // - if the literal contains a decimal point or the exponential
            //   symbol (E), it is a `double`.
            // - otherwise, the literal's type is the first type in this
            //   list that can the literal's value:
            //      * `int`   : from -2x10^9 up to +2x10^9,
            //      * `uint`  : from +2x10^9 up to +4x10^9,
            //      * `long`  : from -9x10^18 up to -2x10^9 and from +4x10^9 up to +9x10^18
            //      * `ulong` : from +9x10^18 up to 18x10^18

            DisplayBarVal(1.0.GetType());           // System.Double (double)
            DisplayBarVal(1E06.GetType());          // System.Double (double)
            DisplayBarVal(1.GetType());             // System.In32 (int)
            DisplayBarVal(0xF0_000_000.GetType());  // System.UInt32 (uint)
            WriteLine(0x100_000_000.GetType());     // System.Int64 (long)

            
            // Numeric suffixes

            // *Numeric suffixes* explicitly define the type of a literal.
            // Suffixes can be either lowercase or uppercase.

            //float f = 1.0F; 
            DisplayBarVal(1F.GetType()); // System.Single (single-precision)

            //double d = 1.0D;
            DisplayBarVal(1D.GetType()); // System.Double (double-precision)

            //decimal dec = 1.0M;
            DisplayBarVal(1M.GetType()); // System.Decimal

            //uint ui = 1U;
            DisplayBarVal(1U.GetType()); // System.UInt32

            //long l = 1L;
            DisplayBarVal(1L.GetType()); // System.Int64

            //ulong ul = 1UL;
            DisplayBarVal(1UL.GetType()); // System.UInt64

            // The suffixes `U` and `L` are rarely necessary, because the
            // the `uint`,  `long`, and `ulong` types can nearly always
            // be either *inferred* or *implicitly converted* from `int`.
            //long i = 5; // implicit lossless conversion from `int` literal to `long`

            // The `D` suffix is technically redundant, in that all literals
            // with a decimal point are inferred to be `double`. And you can
            // always add a decimal point to a numeric literal.
            //double x = 4.0;

            // The `F` and `M` suffixes are the most useful and should always
            // be applied when specifying `float` or `decimal` literals.

            // Without the `F` suffix, the following line would not compile,
            // because 4.5 would be inferred to be of type `double`, which
            // has no implicit conversion to `float`.
            //float f = 4.5F;

            // The same principle is true for a decimal literal.
            //decimal d = -1.23M; // will not compile without the M suffix
        }
    }
}
