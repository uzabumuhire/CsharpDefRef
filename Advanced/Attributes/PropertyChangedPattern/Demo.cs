using System.ComponentModel;

using static Core.ConsoleHelper;

namespace Advanced.Attributes.PropertyChangedPattern
{
    static class Demo
    {
        /// <summary>
        /// Demonstrates usage of the property changed pattern.
        /// </summary>
        internal static void Test()
        {
            Customer c = new Customer();

            c.PropertyChanged += customer_PropertyChanged;

            c.Name = "Avery Rose Isimbi Nefertiti Uzabumuhire";
            c.Gender = "Female";
        }

        static void customer_PropertyChanged(object sender, PropertyChangedEventArgs eventArgs)
        {
            DisplayInfo("Customer's " + eventArgs.PropertyName + " has been modified.");
        }
    }
}
