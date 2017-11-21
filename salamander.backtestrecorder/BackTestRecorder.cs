using com.defrobo.salamander;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;

namespace salamander.backtester
{
    public class BackTestRecorder
    {
        private readonly Dictionary<long, List<Execution>> executions = new Dictionary<long, List<Execution>>();
        private readonly Dictionary<long, List<OrderBookUpdate>> orderBookUpdates = new Dictionary<long, List<OrderBookUpdate>>();
        private readonly Dictionary<long, List<OrderBookUpdate>> orderBookSnapshots = new Dictionary<long, List<OrderBookUpdate>>();
        private readonly Dictionary<long, List<MarketTick>> marketTicks = new Dictionary<long, List<MarketTick>>();

        private readonly ExecutionAlerter executionAlerter;
        private readonly OrderBookUpdater orderBookUpdater;
        private readonly Ticker ticker;

        private DateTime startTime;

        public BackTestRecorder(ExecutionAlerter executionAlerter, OrderBookUpdater orderBookUpdater, Ticker ticker)
        {
            this.executionAlerter = executionAlerter;
            this.orderBookUpdater = orderBookUpdater;
            this.ticker = ticker;
        }

        public void Record()
        {
            startTime = DateTime.Now;
            Console.WriteLine("Recording");
            executionAlerter.ExecutionCreated += ExecutionAlerter_ExecutionCreated;
            orderBookUpdater.OrderBookUpdated += OrderBookUpdater_OrderBookUpdated;
            orderBookUpdater.OrderBookSnapshot += OrderBookUpdater_OrderBookSnapshot;
            ticker.TickerUpdated += Ticker_TickerUpdated;
            executionAlerter.Start();
            orderBookUpdater.Start();
            ticker.Start();

        }

        private void Ticker_TickerUpdated(object sender, MarketTickEventArgs e)
        {
            var elapsedTicks = GetElapsedTicks();
            Console.Write("mt ");

            var marketTick = new MarketTick[] { e.Tick };

            if (!marketTicks.ContainsKey(elapsedTicks))
            {
                marketTicks.Add(elapsedTicks, new List<MarketTick>(marketTick));
            }
            else
            {
                marketTicks[elapsedTicks].AddRange(marketTick);
            }
        }

        private void OrderBookUpdater_OrderBookSnapshot(object sender, OrderBookSnapshotEventArgs e)
        {
            Console.Write("obs ");
            var elapsedTicks = GetElapsedTicks();

            var orderBookUpdate = new OrderBookUpdate[] { e.OrderBookSnapshot };

            if (!orderBookSnapshots.ContainsKey(elapsedTicks))
            {
                orderBookSnapshots.Add(elapsedTicks, new List<OrderBookUpdate>(orderBookUpdate));
            }
            else
            {
                orderBookSnapshots[elapsedTicks].AddRange(orderBookUpdate);
            }
        }

        private void OrderBookUpdater_OrderBookUpdated(object sender, OrderBookUpdateEventArgs e)
        {
            Console.Write("obu ");
            var elapsedTicks = GetElapsedTicks();

            var orderBookUpdate = new OrderBookUpdate[] { e.OrderBookUpdate };

            if (!orderBookUpdates.ContainsKey(elapsedTicks))
            {
                orderBookUpdates.Add(elapsedTicks, new List<OrderBookUpdate>(orderBookUpdate));
            }
            else
            {
                orderBookUpdates[elapsedTicks].AddRange(orderBookUpdate);
            }
        }

        private void ExecutionAlerter_ExecutionCreated(object sender, ExecutionEventArgs e)
        {
            Console.Write("ex ");
            var elapsedTicks = GetElapsedTicks();

            if (!executions.ContainsKey(elapsedTicks))
            {
                executions.Add(elapsedTicks, new List<Execution>(e.Executions));
            }
            else
            {
                executions[elapsedTicks].AddRange(e.Executions);
            }
        }

        private long GetElapsedTicks()
        {
            return DateTime.Now.Subtract(startTime).Ticks;
        }

        public void Save(string filePath)
        {
            executionAlerter.Stop();
            orderBookUpdater.Stop();
            ticker.Stop();

            var backTestData = new BackTestData()
            {
                Executions = executions,
                OrderBookUpdates = orderBookUpdates,
                OrderBookSnapshots = orderBookSnapshots,
                MarketTicks = marketTicks
            };

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                Serializer.Serialize<BackTestData>(fileStream, backTestData);
            }
        }
    }
}
