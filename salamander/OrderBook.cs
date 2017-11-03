using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.defrobo.salamander
{
    public class OrderBook
    {
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

        public OrderBook()
        {
            bids = new SortedList<decimal, Order>();
            asks = new SortedList<decimal, Order>();
        }

        public void Update(OrderBookUpdate update)
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
