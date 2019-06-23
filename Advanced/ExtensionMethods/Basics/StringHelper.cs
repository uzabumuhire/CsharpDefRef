namespace Advanced.ExtensionMethods.Basics
{
    static class StringHelper
    {
        /// <summary>
        /// An extension method to check whether a string
        /// has the first letter capitalized.
        /// </summary>
        /// <param name="s">The string to be checked</param>
        /// <returns>true, if <paramref name="s"/> is capitalized, false otherwise.</returns>
        internal static bool IsCapitalized(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;

            return char.IsUpper(s[0]);
        }
    }
}
