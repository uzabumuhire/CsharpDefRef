using static System.Console;
using static System.ConsoleColor;

namespace Core
{
    public static class ConsoleHelper
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

        public static void DisplayWarning(string message)
        {
            DisplayFormatedMessage("warning", message, Yellow, Black, Yellow);
        }

        public static void DisplayDanger(string message)
        {
            DisplayFormatedMessage("danger", message, Red, White, Red);
        }

        public static void DisplayInfo(string message)
        {
            DisplayFormatedMessage("info", message, Blue, White, Cyan);
        }

        public static void DisplayAlert(string message)
        {
            DisplayFormatedMessage("alert", message, Green, White, Green);
        }

        public static void DisplayError(string message)
        {
            DisplayFormatedMessage("error", message, Red, White, Red);
        }

        public static void DisplayThreadInfo(string message)
        {
            DisplayFormatedMessage("thread info", message, DarkCyan, White, DarkCyan);
        }

        static void DisplayFormatedMessage(
            string messageType,
            string message, 
            System.ConsoleColor titleBackgroundColor,
            System.ConsoleColor titleForegroudColor ,
            System.ConsoleColor messageColor)
        {
            ResetColor();

            BackgroundColor = titleBackgroundColor;
            ForegroundColor = titleForegroudColor;

            System.Text.StringBuilder m = new System.Text.StringBuilder(" ");
            m.Insert(1, messageType.Trim().ToUpper());

            int l = 9 - messageType.Trim().ToUpper().Length;
            for (int i = 0; i < l; i++)
                m.Append(' ');

            m.Append(':');
            Write(m);
            m.Clear();

            ResetColor();

            ForegroundColor = messageColor;
            m.Append(' ');
            m.Append(message.Trim());
            WriteLine(m);

            ResetColor();
        }
    }
}
