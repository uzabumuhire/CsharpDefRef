using static System.Console;
namespace Core
{
    public static class Utility
    {
        public static void DisplaySpaceVal<T>(T val)
        {
            Write(val + " ");
        }

        public static void DisplayVal<T>(T val, string separator)
        {
            Write(val + separator);
        }

        public static void DisplayBar()
        {
            Write(" | ");
        }
    }
}
