using System;
using System.Linq;

namespace com.defrobo.salamander
{
    class Program
    {
        private static decimal lastTradedPrice;
        private static decimal bestAsk;
        private static decimal bestBid;

        private static OrderBook orderBook;

        static void Main(string[] args)
        {
            //var ticker = new Ticker();
            //var executionAlerter = new ExecutionAlerter();
            var orderBookUpdater = new OrderBookUpdater();
            orderBook = new OrderBook(orderBookUpdater);
            //var logger = new ScreenMarketTicketLogger(ticker);
            //ticker.Updated += Ticker_Updated;
            //executionAlerter.Created += ExecutionAlerter_Created;
            orderBookUpdater.Snapshot += OrderBookUpdater_Refresh;
            orderBookUpdater.Updated += OrderBookUpdater_Refresh;
            //logger.Start();
            //ticker.Start();
            //executionAlerter.Start();
            orderBookUpdater.Start();
            string resp = Console.ReadLine();
            //logger.Stop();
            //ticker.Stop();
            //executionAlerter.Stop();
            orderBookUpdater.Stop();
            string resp2 = Console.ReadLine();
        }

        private static void OrderBookUpdater_Refresh(object sender, OrderBookUpdateEventArgs e)
        {
            Console.Clear();
            if (orderBook.Bids.Count == 0 || orderBook.Asks.Count == 0)
                return;

            var bestBidOrder = orderBook.Bids.Reverse().First().Value;
            var bestAskOrder = orderBook.Asks.First().Value;
            Console.WriteLine("best ask:     " + bestAskOrder.Price + " - " + bestAskOrder.Size  + "BTC");
            Console.Write("OBOOK mprice: " + orderBook.MidPrice + " ");
            Console.Write("TICK LTP " + lastTradedPrice + " ask: " + bestAsk + " bid: " + bestBid + " ");
            if (lastTradedPrice == bestBid) Console.WriteLine(" BID");
            else if (lastTradedPrice == bestAsk) Console.WriteLine(" ASK");
            else Console.WriteLine("???");
            Console.WriteLine("best bid:     " + bestBidOrder.Price + " - " + bestBidOrder.Size + " BTC");
        }

        private static void ExecutionAlerter_Created(object sender, ExecutionEventArgs e)
        {
            for (int i = 0; i < e.Executions.Length; i++)
            {
                Console.WriteLine("EXEC " + e.Executions[i].Size + " @ " + e.Executions[i].Price + e.Executions[i].Side);
            }
        }

        private static void Ticker_Updated(object sender, MarketTickEventArgs e)
        {
            if (e.Tick.LastTradedPrice == lastTradedPrice && e.Tick.BestAsk == bestAsk && e.Tick.BestBid == bestBid)
                return;

            lastTradedPrice = e.Tick.LastTradedPrice;
            bestAsk = e.Tick.BestAsk;
            bestBid = e.Tick.BestBid;

        }
    }
}
