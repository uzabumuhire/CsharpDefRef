using System.ComponentModel;

using static Core.Utility;

namespace Advanced.Attributes.PropertyChangedPattern
{
    static class Demo
    {
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
