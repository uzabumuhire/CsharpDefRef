using static System.Console;

using static Core.Utility;

namespace Advanced
{
    class Program
    {
        static void Main(string[] args)
        {
            // DELEGATES
            WriteLine("DELEGATES");
            WriteLine();
            DelegatesDemo();

            // EVENTS
            WriteLine();
            WriteLine();
            WriteLine("EVENTS");
            WriteLine();
            EventsDemo();
        }

        static void DelegatesDemo()
        {
            Delegates.Basics.Demo.Test();

            WriteLine();

            Delegates.PlugInMethods.Demo.Test();

            WriteLine();

            Delegates.Multicasts.Demo.Test();

            WriteLine();

            Delegates.InstanceMethodTarget.Demo.Test();

            WriteLine();

            Delegates.GenericParameters.Demo.Test();

            WriteLine();

            Delegates.FuncAction.Demo.Test();

            WriteLine();

            Delegates.Interfaces.Demo.Test();

            WriteLine();

            Delegates.Compatibility.Demo.Types();
            DisplayBar();
            Delegates.Compatibility.Demo.Contravariance();
            DisplayBar();
            Delegates.Compatibility.Demo.Covariance();
        }

        static void EventsDemo()
        {
            Events.Basics.Demo.Test();

            WriteLine();

            Events.StandardPattern.Demo.Test();

            WriteLine();

            Events.NonGenericHandler.Demo.Test();
        }
    }
}
