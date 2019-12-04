using System;
using System.Text;
using System.Threading.Tasks;

using static System.Console;
using static System.ConsoleColor;

namespace Core
{
    public static class ConsoleHelper
    {
        /// <summary>
        /// Displays a value followed by a space.
        /// </summary>
        /// <typeparam name="T">The type of the <paramref name="value"/>.</typeparam>
        /// <param name="value">The value to display.</param>
        public static void DisplaySpaceVal<T>(T value)
        {
            Write($"{value} ");
        }

        /// <summary>
        /// Displays a value followed by a bar and space.
        /// </summary>
        /// <typeparam name="T">The type of the <paramref name="value"/>.</typeparam>
        /// <param name="value">The value to display.</param>
        public static void DisplayBarVal<T>(T value)
        {
            Write($"{value} | ");
        }

        /// <summary>
        /// Displays a value followed by a separator.
        /// </summary>
        /// <typeparam name="T">The type of the <paramref name="value"/>.</typeparam>
        /// <param name="value">The value to display.</param>
        /// <param name="separator">The seperator.</param>
        public static void DisplayVal<T>(T value, string separator)
        {
            Write($"{value}{separator}");
        }

        /// <summary>
        /// Displays a bar.
        /// </summary>
        public static void DisplayBar()
        {
            Write(" | ");
        }

        /// <summary>
        /// Displays a warning message.
        /// </summary>
        /// <param name="message">A warning message.</param>
        public static void DisplayWarning(string message)
        {
            DisplayFormatedMessage("warning", message, Yellow, Black, Yellow);
        }

        /// <summary>
        /// Displays a danger message.
        /// </summary>
        /// <param name="message">A danger message.</param>
        public static void DisplayDanger(string message)
        {
            DisplayFormatedMessage("danger", message, Red, White, Red);
        }

        /// <summary>
        /// Displays an info message.
        /// </summary>
        /// <param name="message">An info message.</param>
        public static void DisplayInfo(string message)
        {
            DisplayFormatedMessage("info", message, Blue, White, Cyan);
        }

        /// <summary>
        /// Displays an alert message.
        /// </summary>
        /// <param name="message">An alert message.</param>
        public static void DisplayAlert(string message)
        {
            DisplayFormatedMessage("alert", message, Green, White, Green);
        }

        /// <summary>
        /// Displays an error message.
        /// </summary>
        /// <param name="message">An error message.</param>
        public static void DisplayError(string message)
        {
            DisplayFormatedMessage("error", message, Red, White, Red);
        }

        /// <summary>
        /// Displays a thread information  message.
        /// </summary>
        /// <param name="message">A thread information message.</param>
        public static void DisplayThreadInfo(string message)
        {
            DisplayFormatedMessage("thread info", message, DarkCyan, White, DarkCyan);
        }

        /// <summary>
        /// Displays a task information message.
        /// </summary>
        /// <param name="message">A task information messge.</param>
        public static void DisplayTaskInfo(string message)
        {
            DisplayFormatedMessage("task info", message, DarkCyan, White, DarkCyan);
        }

        /// <summary>
        /// Displays a method information message.
        /// </summary>
        /// <param name="message">A method information message.</param>
        public static void DisplayMethodInfo(string message)
        {
            DisplayFormatedMessage("method info", message, DarkCyan, White, DarkCyan);
        }

        /// <summary>
        /// Displays a <see cref="Console"/> formated message.
        /// </summary>
        /// <param name="messageType">The type of the message to display.</param>
        /// <param name="message">The message to display.</param>
        /// <param name="titleBackgroundColor">The background color of the message title.</param>
        /// <param name="titleForegroudColor">The foreground color of the message title.</param>
        /// <param name="messageColor">The message content color.</param>
        static void DisplayFormatedMessage(
            string messageType,
            string message, 
            ConsoleColor titleBackgroundColor,
            ConsoleColor titleForegroudColor ,
            ConsoleColor messageColor)
        {
            WriteLine();
            WriteLine();

            ResetColor();

            BackgroundColor = titleBackgroundColor;
            ForegroundColor = titleForegroudColor;

            StringBuilder m = new StringBuilder(" ");
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

            WriteLine();
        }

        /// <summary>
        /// Displays a demo result with a message.
        /// </summary>
        /// <param name="message">A message for the <paramref name="demo"/>.</param>
        /// <param name="demo">The demo to call.</param>
        public static void DisplayDemo(string message, Action demo)
        {
            WriteLine();
            WriteLine();
            WriteLine(message);
            WriteLine();
            demo();
        }

        /// <summary>
        /// Displays a demo result with a message.
        /// </summary>
        /// <param name="message">A message for the <paramref name="demo"/>.</param>
        /// <param name="demo">The demo to call.</param>
        /// <returns>A task to signal upon completion or fault.</returns>
        public static async Task DisplayDemoAsync(string message, Func<Task> demo)
        {
            WriteLine();
            WriteLine();
            WriteLine(message);
            WriteLine();
            await demo();
        }
    }
}
