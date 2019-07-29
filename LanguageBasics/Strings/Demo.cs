using static System.Console;

using static Core.ConsoleHelper;

namespace Basics.Strings
{
    static class Demo
    {
        /// <summary>
        /// Demonstrates basic usage of strings and characters.
        /// </summary>
        internal static void Run()
        {
            UnicodeCharacter();

            WriteLine();

            Fundamentals();
        }

        /// <summary>
        /// Demonstrates string fundementals.
        /// </summary>
        static void Fundamentals()
        {
            // C#'s `string` type (aliasing the `System.String` type
            // represents an immutable sequence of Unicode characters.
            // A `string` literal is specified inside double quotes.

            // `string` is a reference type rather than a value type.
            // However, its equality operators follow value-type
            // semantics.
            string a = "test";
            string b = "test";
            DisplayVal(a == b, " | ");

            // The escape sequence that are valid for `char` also
            // work inside strings. The cost of this is that whenever
            // you need a literal backslash, you must write it twice.
            string uri1 = "\\\\server\\fileshar\\filename.ext";
            DisplayVal(uri1, " | ");

            // To avoid the backslah problem, use *verbatim* string
            // literals. A verbatim string literal is prefixed with
            // @ and does not support sequences.
            string uri2 = @"\\server\fileshare\filename.ext";
            DisplayVal(uri2, " | ");

            // A verbatim string can also span multiple lines.
            string escaped = "First line\r\nSecond line";
            DisplayVal(escaped, " | ");

            string verbatim = @"First line
Second line";
            DisplayVal(verbatim, " | ");

            // Prints true if your IDE uses CR-LF line separators.
            DisplayVal(escaped == verbatim, " | ");

            DisplayVal(IsCRLFUsedByIDE(), " | ");

            // Use double-quote character in a verbatim by writing
            // it twice.
            string xml = @"<customer id=""123""></customer>";
            DisplayVal(xml, " | ");

            // The `+` operator concatenates two strings. One of the
            // the value may be a nonstring value, in which case
            // `ToString` is called on that value.

            // Using the `+` operator to build up a string is
            // inefficient : a better solution is to use the
            // `System.Text.StringBuilder` type.

            // A string preceded with the $ character is called an
            // *interpolated string*. An interpolated string  can
            // include expressions inside braces.

            int x = 4;
            string s1 = $"A square has {x} sides";
            DisplayVal(s1, " | ");

            // Any  valid C# expression of any type can appear
            // within braces, and C# will convert the expression
            // to a string by calling its `ToString` method or
            // equivalent. You can change the formating by
            // appending the expression with a colon and a format.

            // X2 = 2-digit Hexadecimal.
            string s2 = $"255 in hex is {byte.MaxValue:X2}";
            DisplayVal(s2, " | ");

            // Interpolated strings must complete on a single line,
            // unless you also specify the verbatim string operator.
            // Note that  the `$` operator must come before `@`.
            int y = 2;
            string s3 = $@"This spans {y}
 lines";
            DisplayVal(s3, " | ");

            // To include a brace literal in a interpolated string,
            // repeat the desired  brace character.

            // `string` does not support < and > operators for
            // comparisons. You must use the `string`'s
            // `CompareTo` method.

        }

        static bool IsCRLFUsedByIDE()
        {
            string escaped = " \r\n ";
            string verbatim = @"
 ";
            return escaped == verbatim;
        }
        
        /// <summary>
        /// Demonstrates usage of Unicode characters.
        /// </summary>
        static void UnicodeCharacter()
        {
            // C#'s `char` type (aliasing the `System.Char` type)
            // represents a Unicode character and occupies
            // 2 bytes (16 bits). A `char` literal is specified
            // inside single quotes.

            // *Escape sequences* expresse characters that cannot
            // be expressed or interpreted literally. An *escape
            // sequence* is a backslash followed by a character
            // with special meaning.

            // The \u or \x escape sequence lets specify any
            // Unicode character via its four-digit
            // hexadecimal code.
            char copyRightSymbol = '\u00A9';
            DisplayVal(copyRightSymbol, " | ");

            char omegaSymbol = '\u03A9';
            DisplayVal(omegaSymbol, "");
        }
    }
}
