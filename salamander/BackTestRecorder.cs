using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;

namespace com.defrobo.salamander
{
    public class BackTestRecorder
    {
        private readonly Dictionary<long, List<ExecutionEventArgs>> executionEvents = new Dictionary<long, List<ExecutionEventArgs>>();
        private readonly Dictionary<long, List<OrderBookUpdateEventArgs>> orderBookUpdateEvents = new Dictionary<long, List<OrderBookUpdateEventArgs>>();
        private readonly Dictionary<long, List<OrderBookSnapshotEventArgs>> orderBookSnapshotEvents = new Dictionary<long, List<OrderBookSnapshotEventArgs>>();
        private readonly Dictionary<long, List<MarketTickEventArgs>> marketTickEvents = new Dictionary<long, List<MarketTickEventArgs>>();

        private readonly IAlerter alerter;

        private DateTime startTime;

        public BackTestRecorder(IAlerter alerter)
        {
            this.alerter = alerter;
        }

        public void Record()
        {
            startTime = DateTime.Now;
            alerter.ExecutionCreated += Alerter_ExecutionCreated;
            alerter.OrderBookUpdated += Alerter_OrderBookUpdated;
            alerter.OrderBookSnapshot += Alerter_OrderBookSnapshot;
            alerter.TickerUpdated += Alerter_TickerUpdated;
            alerter.Start();
        }

        private void Alerter_TickerUpdated(object sender, MarketTickEventArgs e)
        {
            var elapsedTicks = GetElapsedTicks();

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

        private void Alerter_OrderBookSnapshot(object sender, OrderBookSnapshotEventArgs e)
        {
            var elapsedTicks = GetElapsedTicks();

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

        private void Alerter_OrderBookUpdated(object sender, OrderBookUpdateEventArgs e)
        {
            var elapsedTicks = GetElapsedTicks();

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

        private void Alerter_ExecutionCreated(object sender, ExecutionEventArgs e)
        {
            var elapsedTicks = GetElapsedTicks();

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
            var backTestData = new BackTestData()
            {
                ExecutionEvents = new Dictionary<long, List<ExecutionEventArgs>>(executionEvents),
                OrderBookUpdateEvents = new Dictionary<long, List<OrderBookUpdateEventArgs>>(orderBookUpdateEvents),
                OrderBookSnapshotEvents = new Dictionary<long, List<OrderBookSnapshotEventArgs>>(orderBookSnapshotEvents),
                MarketTickEvents = new Dictionary<long, List<MarketTickEventArgs>>(marketTickEvents)
            };

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                Serializer.Serialize<BackTestData>(fileStream, backTestData);
            }
        }

        public void Clear()
        {
            executionEvents.Clear();
            orderBookUpdateEvents.Clear();
            orderBookSnapshotEvents.Clear();
            marketTickEvents.Clear();
        }

        public void Stop()
        {
            alerter.Stop();
        }
    }
}
