using System;

using static System.String;

namespace Collections.CustomizableProxies.SimpleOrder
{
    /// <summary>
    /// OrderItem represents a simple line item in an order. 
    /// </summary>
    class OrderItem
    {
        // All the values are immutable except quantity.
        internal readonly int PartNumber;
        internal readonly string Description;
        internal readonly double UnitPrice;

        int _quantity = 0;
        internal int Quantity
        {
            get { return _quantity; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Quantity cannot be negative.");

                _quantity = value;
            }
        }

        internal OrderItem(
            int partNumber, string description,
            int quantity, double unitPrice)
        {
            this.PartNumber = partNumber;
            this.Description = description;
            this.Quantity = quantity;
            this.UnitPrice = unitPrice;
        }

        public override string ToString()
        {
            return Format(
                "{0,9} {1,6} {2,-12} at {3,8:#,###.00} = {4,10:###,###.00}",
                PartNumber, _quantity, Description, UnitPrice,
                UnitPrice * _quantity);
        }
    }
}
