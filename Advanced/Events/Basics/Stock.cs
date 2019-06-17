using System;

namespace Advanced.Events.Basics
{
    // Delegate definition.
    delegate void PriceChangedHandler(object sender, PriceChangedEventArgs eventArgs);

    /// <summary>
    /// Stock is broadcaster type that fires its `PriceChanged` event
    /// every time the `Price` of the `Stock` changes.
    /// </summary>
    class Stock
    {
        string symbol;

        decimal price;
        internal decimal Price
        {
            get { return price; }
            set
            {
                if (price == value)
                    return; // Exit if nothing has changed
                decimal oldPrice = price;
                price = value;
                OnPriceChanged(new PriceChangedEventArgs(oldPrice, price));
            }
        }

        // Event declaration : put the `event` keyword in
        // front of a delegate member.
        // If we remove the `event` keyword so that `PriceChanged`
        // becomes an ordinary delegate field, the code would give
        // the same results. However `Stock` would be less robust, 
        // in that subscribers would interfere with one another.
        internal event PriceChangedHandler PriceChanged;

        /*
        The compiler tranlates the event declaration into 
        something close to the following.
        PriceChangedHandler priceChanged; // private delegate;
        internal event PriceChangedHandler PriceChanged
        {
            add { priceChanged += value; }
            remove { priceChanged -= value; }
        }
        */

        internal Stock(string symbol)
        {
            this.symbol = symbol;
        }

        protected virtual void OnPriceChanged(PriceChangedEventArgs eventArgs)
        {
            /*
                if (PriceChanged != null)
                {
                    // If invocation list not empty, fire event
                    PriceChanged(oldPrice, price);
                }
                */
            // If invocation list not empty, fire event
            PriceChanged?.Invoke(this, eventArgs);
        }
    }
}
