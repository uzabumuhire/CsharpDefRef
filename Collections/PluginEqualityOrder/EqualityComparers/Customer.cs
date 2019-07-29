using System;
namespace Collections.PluginEqualityOrder.EqualityComparers
{
    /// <summary>
    /// Defines a customer.
    /// </summary>
    class Customer
    {
        public string LastName;
        public string FirstName;

        public Customer(string lastName, string firstName)
        {
            LastName = lastName;
            FirstName = firstName;
        }
    }
}
