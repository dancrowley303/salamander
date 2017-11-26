using System.Collections.Generic;
using System.Linq;

namespace com.defrobo.salamander
{
    public class OrderBook
    {
        private readonly IOrderBookUpdater orderBookUpdater;
        private readonly SortedList<decimal, Order> bids;
        private readonly SortedList<decimal, Order> asks;

        public List<Order> Bids
        {
            get
            {
                var revList = bids.Values.ToList();
                revList.Reverse();
                return revList;
            }
        }

        public List<Order> Asks

        {
            get
            {
                return asks.Values.ToList();
            }
        }

        public decimal MidPrice { get; private set; }

        public OrderBook(IOrderBookUpdater orderBookUpdater)
        {
            this.orderBookUpdater = orderBookUpdater;
            this.orderBookUpdater.OrderBookSnapshot += OrderBookUpdater_Snapshot;
            this.orderBookUpdater.OrderBookUpdated += OrderBookUpdater_Updated;
            bids = new SortedList<decimal, Order>();
            asks = new SortedList<decimal, Order>();
        }

        private void OrderBookUpdater_Updated(object sender, OrderBookUpdateEventArgs e)
        {
            Update(e.OrderBookUpdate);
        }

        private void OrderBookUpdater_Snapshot(object sender, OrderBookSnapshotEventArgs e)
        {
            Update(e.OrderBookSnapshot);
        }

        private void Update(OrderBookUpdate update)
        {
            this.MidPrice = update.MidPrice;
            if (update.Asks != null)
            {
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
            }

            if (update.Bids != null)
            {
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
}
