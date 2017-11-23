using com.defrobo.salamander;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;

namespace salamander.backtester
{
    public class BackTestRecorder
    {
        private readonly Dictionary<long, List<ExecutionEventArgs>> executionEvents = new Dictionary<long, List<ExecutionEventArgs>>();
        private readonly Dictionary<long, List<OrderBookUpdateEventArgs>> orderBookUpdateEvents = new Dictionary<long, List<OrderBookUpdateEventArgs>>();
        private readonly Dictionary<long, List<OrderBookSnapshotEventArgs>> orderBookSnapshotEvents = new Dictionary<long, List<OrderBookSnapshotEventArgs>>();
        private readonly Dictionary<long, List<MarketTickEventArgs>> marketTickEvents = new Dictionary<long, List<MarketTickEventArgs>>();

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

            var marketTickEventArgs = new MarketTickEventArgs[] { e };

            if (!marketTickEvents.ContainsKey(elapsedTicks))
            {
                marketTickEvents.Add(elapsedTicks, new List<MarketTickEventArgs>(marketTickEventArgs));
            }
            else
            {
                marketTickEvents[elapsedTicks].AddRange(marketTickEventArgs);
            }
        }

        private void OrderBookUpdater_OrderBookSnapshot(object sender, OrderBookSnapshotEventArgs e)
        {
            var elapsedTicks = GetElapsedTicks();
            Console.Write("obs ");

            var orderBookSnapshotEventArgs = new OrderBookSnapshotEventArgs[] { e };

            if (!orderBookSnapshotEvents.ContainsKey(elapsedTicks))
            {
                orderBookSnapshotEvents.Add(elapsedTicks, new List<OrderBookSnapshotEventArgs>(orderBookSnapshotEventArgs));
            }
            else
            {
                orderBookSnapshotEvents[elapsedTicks].AddRange(orderBookSnapshotEventArgs);
            }
        }

        private void OrderBookUpdater_OrderBookUpdated(object sender, OrderBookUpdateEventArgs e)
        {
            var elapsedTicks = GetElapsedTicks();
            Console.Write("obu ");

            var orderBookUpdateEventArgs = new OrderBookUpdateEventArgs[] { e };

            if (!orderBookUpdateEvents.ContainsKey(elapsedTicks))
            {
                orderBookUpdateEvents.Add(elapsedTicks, new List<OrderBookUpdateEventArgs>(orderBookUpdateEventArgs));
            }
            else
            {
                orderBookUpdateEvents[elapsedTicks].AddRange(orderBookUpdateEventArgs);
            }
        }

        private void ExecutionAlerter_ExecutionCreated(object sender, ExecutionEventArgs e)
        {
            var elapsedTicks = GetElapsedTicks();
            Console.Write("ex ");

            var executionEventArgs = new ExecutionEventArgs[] { e };

            if (!executionEvents.ContainsKey(elapsedTicks))
            {
                executionEvents.Add(elapsedTicks, new List<ExecutionEventArgs>(executionEventArgs));
            }
            else
            {
                executionEvents[elapsedTicks].AddRange(executionEventArgs);
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
                ExecutionEvents = executionEvents,
                OrderBookUpdateEvents = orderBookUpdateEvents,
                OrderBookSnapshotEvents = orderBookSnapshotEvents,
                MarketTickEvents = marketTickEvents
            };

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                Serializer.Serialize<BackTestData>(fileStream, backTestData);
            }
        }
    }
}
