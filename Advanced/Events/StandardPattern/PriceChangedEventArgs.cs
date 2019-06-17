using System;
namespace Advanced.Events.StandardPattern
{
    /// <summary>
    /// Conveys information (the old and new prices) when a PriceChanged is fired.
    /// To convey information for an event, it subclasses <see cref="EventArgs"/>.
    /// </summary>
    class PriceChangedEventArgs : EventArgs
    {
        internal readonly decimal LastPrice;
        internal readonly decimal NewPrice;

        internal PriceChangedEventArgs(decimal lastPrice, decimal newPrice)
        {
            LastPrice = lastPrice;
            NewPrice = newPrice;
        }
    }
}
