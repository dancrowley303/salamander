using System;

namespace com.defrobo.salamander
{
    public class ScreenMarketTicketLogger : IMarketTickLogger
    {
        private readonly ITicker ticker;

        public ScreenMarketTicketLogger(ITicker ticker)
        {
            this.ticker = ticker;
        }

        public void Start()
        {
            ticker.TickerUpdated += Ticker_Updated;
        }

        public void Stop()
        {
            ticker.TickerUpdated -= Ticker_Updated;
        }

        private void Ticker_Updated(object sender, MarketTickEventArgs e)
        {
            Console.WriteLine(e.RawTickMessage);
        }

    }
}
