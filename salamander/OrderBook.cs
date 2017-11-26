using System.Collections.Generic;
using System.Linq;

namespace com.defrobo.salamander
{
    public class OrderBook
    {
        private readonly IAlerter alerter;
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

        public OrderBook(IAlerter alerter)
        {
            this.alerter = alerter;
            this.alerter.OrderBookSnapshot += Alerter_OrderBookSnapshot;
            this.alerter.OrderBookUpdated += Alerter_OrderBookUpdated;
            bids = new SortedList<decimal, Order>();
            asks = new SortedList<decimal, Order>();
        }

        private void Alerter_OrderBookUpdated(object sender, OrderBookUpdateEventArgs e)
        {
            Update(e.OrderBookUpdate);
        }

        private void Alerter_OrderBookSnapshot(object sender, OrderBookSnapshotEventArgs e)
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
