using System;
namespace Advanced.Events.StandardPattern
{
    // A delegate provided by the .NET to help in implementing the standard event pattern. No need define a delegate.
    //delegate void EventHandler<TEventArgs>(object source, TEventArgs eventArgs) where TEventArgs : EventArgs;
    // Before generics (prior to C# 2.0), we would have to write a custom delegate as follows:
    //delegate void PriceChangedEventHandler(object sender, PriceChangedEventArgs eventArgs);

    internal class Stock
    {
        // Define an event by using the generic `EventHandler` delegate.
        internal event EventHandler<PriceChangedEventArgs> PriceChanged;

        string symbol;

        decimal price;
        internal decimal Price
        {
            get { return price; }
            set
            {
                if (price == value)
                    return;
                decimal oldPrice = price;
                price = value;
                OnPriceChanged(new PriceChangedEventArgs(oldPrice, price));
            }
        }

        internal Stock(string symbol)
        {
            this.symbol = symbol;
        }

        /// <summary>
        /// Fires the PriceChanged event. 
        /// </summary>
        /// <param name="eventArgs">The information being conveyed by the event.</param>
        protected virtual void OnPriceChanged(PriceChangedEventArgs eventArgs)
        {
            PriceChanged?.Invoke(this, eventArgs);
        }
    }
}
