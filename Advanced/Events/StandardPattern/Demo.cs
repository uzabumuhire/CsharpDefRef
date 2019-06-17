using static Core.Utility;

namespace Advanced.Events.StandardPattern
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

        static void stock_PriceChanged(object sender, PriceChangedEventArgs eventArgs)
        {
            decimal increase = (eventArgs.NewPrice - eventArgs.LastPrice) / eventArgs.LastPrice;
            decimal decrease = (eventArgs.LastPrice - eventArgs.NewPrice) / eventArgs.LastPrice;

            if (increase >= 0.1M)
            {
                DisplayAlert("more than 10% stock price increase !");
            }
            else if (decrease >= 0.1M)
            {
                DisplayDanger("more than 10% stock price decrease !");
            }
            else if (0M < increase && increase < 0.1M)
            {
                DisplayInfo("less than 10% stock price increase");
            }
            else if (0M < decrease && decrease < 0.1M)
            {
                DisplayWarning("less than 10% stock price decrease");
            }
        }
    }
}
