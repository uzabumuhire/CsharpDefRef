using System;

namespace Advanced.Events.NonGenericHandler
{
    // The predefined non generic EventHandler.
    //public delegate void EventHandler(object source, EventArgs eventArgs);

    /// <summary>
    /// Uses the predefined nongeneric <see cref="EventHandler"/> delegate
    /// because the PriceChanged event does not carry extra information. 
    /// PriceChanged event is fired after the price changes, and no information
    /// about the event is neecessary, other than it happened. 
    /// </summary>
    class Stock
    {
        string symbol;

        internal event EventHandler PriceChanged;

        decimal price;
        internal decimal Price
        {
            get
            {
                return price;
            }

            set
            {
                if (price == value)
                    return;
                price = value;
                // Uses `EventArgs.Empty` property, 
                // in order to avoid unnecessarily
                // instantiating an instance of 
                // `EventArgs`.
                OnPriceChanged(EventArgs.Empty);
            }
        }

        internal Stock( string symbol)
        {
            this.symbol = symbol;
        }

        protected virtual void OnPriceChanged(EventArgs eventArgs)
        {
            PriceChanged?.Invoke(this, eventArgs);
        }

    }
}
