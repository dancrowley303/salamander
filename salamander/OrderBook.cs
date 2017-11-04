using System.Collections.Generic;

namespace com.defrobo.salamander
{
    public class OrderBook
    {
        private readonly OrderBookUpdater orderBookUpdater;
        private readonly SortedList<decimal, Order> bids;
        private readonly SortedList<decimal, Order> asks;

        public SortedList<decimal, Order> Bids
        {
            get
            {
                return bids;
            }
        }

        public SortedList<decimal, Order> Asks

        {
            get
            {
                return asks;
            }
        }

        public decimal MidPrice { get; private set; }

        public OrderBook(OrderBookUpdater orderBookUpdater)
        {
            this.orderBookUpdater = orderBookUpdater;
            this.orderBookUpdater.Snapshot += OrderBookUpdater_Snapshot;
            this.orderBookUpdater.Updated += OrderBookUpdater_Updated;
            bids = new SortedList<decimal, Order>();
            asks = new SortedList<decimal, Order>();
        }

        private void OrderBookUpdater_Updated(object sender, OrderBookUpdateEventArgs e)
        {
            Update(e.OrderBookUpdate);
        }

        private void OrderBookUpdater_Snapshot(object sender, OrderBookUpdateEventArgs e)
        {
            Update(e.OrderBookUpdate);
        }

        private void Update(OrderBookUpdate update)
        {
            this.MidPrice = update.MidPrice;
            foreach (var ask in update.Asks)
            {
                if (ask.Size == 0.0m)
                {
                    asks.Remove(ask.Price);
                }
                else
                {
                    asks[ask.Price] = ask;
                }
            }
            foreach (var bid in update.Bids)
            {
                if (bid.Size == 0.0m)
                {
                    bids.Remove(bid.Price);
                }
                else
                {
                    bids[bid.Price] = bid;
                }
            }
        }
    }
}
