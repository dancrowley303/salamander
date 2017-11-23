using com.defrobo.salamander;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace salamander.backtester
{
    class Program
    {
        private static ExecutionAlerter executionAlerter = new ExecutionAlerter();
        private static OrderBookUpdater orderBookUpdater = new OrderBookUpdater();
        private static Ticker ticker = new Ticker();

        static void Main(string[] args)
        {
            //Record();
            Replay();
        }

        static void Replay()
        {
            var backTestReplayer = new BackTestReplayer(".\\recording.bts");
            backTestReplayer.Replay();
            backTestReplayer.ExecutionCreated += BackTestReplayer_ExecutionCreated;
            backTestReplayer.OrderBookSnapshot += BackTestReplayer_OrderBookSnapshot;
            backTestReplayer.OrderBookUpdated += BackTestReplayer_OrderBookUpdated;
            backTestReplayer.TickerUpdated += BackTestReplayer_TickerUpdated;
            Console.ReadLine();

        }

        private static void BackTestReplayer_TickerUpdated(object sender, MarketTickEventArgs e)
        {
            Console.WriteLine("{0} {1} {2} {3}", e.Tick.TimeStamp, e.Tick.LastTradedPrice, e.Tick.ProductCode, e.Tick.Volume);
        }

        private static void BackTestReplayer_OrderBookUpdated(object sender, OrderBookUpdateEventArgs e)
        {
            Console.WriteLine("{0} {1} {2}", e.OrderBookUpdate.MidPrice, e.OrderBookUpdate.Asks?.Length, e.OrderBookUpdate.Bids?.Length);
        }

        private static void BackTestReplayer_OrderBookSnapshot(object sender, OrderBookSnapshotEventArgs e)
        {
            Console.WriteLine("{0}", e.OrderBookSnapshot.MidPrice);
        }

        private static void BackTestReplayer_ExecutionCreated(object sender, ExecutionEventArgs e)
        {
            foreach(var ex in e.Executions)
            {
                Console.WriteLine("{0} {1} {2} {3}", ex.ID, ex.Price, ex.Size, ex.Side);
            }
        }

        static void Record()
        {
            var backTestRecorder = new BackTestRecorder(executionAlerter, orderBookUpdater, ticker);
            backTestRecorder.Record();
            Console.ReadLine();
            Console.WriteLine("Saving data");
            backTestRecorder.Save(".\\recording.bts");
        }
    }
}
