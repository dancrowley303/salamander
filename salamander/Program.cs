using System;
using System.Collections.Generic;
using System.Linq;

namespace com.defrobo.salamander
{
    class Program
    {
        private static decimal lastTradedPrice;
        private static decimal bestAsk;
        private static decimal bestBid;
        private static Dictionary<Currency, Balance> balances;

        private static BackTestReplayer replayer;

        private static OrderBook orderBook;

        static void Main(string[] args)
        {
            var infoService = new InfoService();
            var balanceResult = infoService.GetBalances();

            replayer = new BackTestReplayer(".\\..\\..\\..\\salamander.backtestrecorder\\bin\\debug\\recording0.bts");

            orderBook = new OrderBook(replayer);
            replayer.TickerUpdated += Ticker_Updated;
            replayer.ExecutionCreated += ExecutionAlerter_Created;
            replayer.OrderBookSnapshot += OrderBookUpdater_Refresh;
            replayer.OrderBookUpdated += OrderBookUpdater_Refresh;

            replayer.Start();


            balances = balanceResult.Result;
            string resp = Console.ReadLine();

        }

        private static void OrderBookUpdater_Refresh(object sender, EventArgs e)
        {
            Console.Clear();
            if (orderBook.Bids.Count == 0 || orderBook.Asks.Count == 0)
                return;

            Console.WriteLine("JPY: {0} / BTC: {1}", balances[Currency.JPY].Amount, balances[Currency.BTC].Amount);

            var bestBidOrder = orderBook.Bids.First();
            var bestAskOrder = orderBook.Asks.First();
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
