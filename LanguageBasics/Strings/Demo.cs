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
        }
        
        /// <summary>
        /// Demonstrates usage of Unicode characters.
        /// </summary>
        static void UnicodeCharacter()
        {
            // C# `char` type (aliasing the `System.Char` type)
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
