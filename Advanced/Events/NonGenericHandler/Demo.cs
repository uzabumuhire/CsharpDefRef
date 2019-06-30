using System;

using static Core.ConsoleHelper;

namespace Advanced.Events.NonGenericHandler
{
    static class Demo
    {
        internal static void Test()
        {
            Stock stock = new Stock("THPW");
            stock.Price = 27.10M;

            // Register with (subscribe to) `PriceChanged` event.
            stock.PriceChanged += stock_PriceChanged;

            // Change stock price
            stock.Price = 31.59M; // increase more than 10%
            stock.Price = 32.59M; // increase lees than 10%
            stock.Price = 27.10M; // decrease more than 10%
            stock.Price = 26.10M; // decrease less than 10%
        }

        static void stock_PriceChanged(object sender, EventArgs eventArgs)
        {
            DisplayInfo("price changed");
        }
    }
}
