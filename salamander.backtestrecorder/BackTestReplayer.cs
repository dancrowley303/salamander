using com.defrobo.salamander;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace salamander.backtester
{
    public class BackTestReplayer : IExecutionAlerter, IOrderBookUpdater, ITicker
    {
        private class OrderBookSnapshotTimerState
        {
            public long Tick { get; set; }
            public OrderBookSnapshotEventArgs OrderBookSnapshotEventArgs { get; set; }
        }

        private class OrderBookUpdateTimerState
        {
            public long Tick { get; set; }
            public OrderBookUpdateEventArgs OrderBookUpdateEventArgs { get; set; }
        }

        private readonly string filePath;
        private Dictionary<ExecutionEventArgs, Timer> executionTimers = new Dictionary<ExecutionEventArgs, Timer>();
        private Dictionary<long, Timer> orderBookSnapshotTimers = new Dictionary<long, Timer>();
        private Dictionary<OrderBookUpdateEventArgs, Timer> orderBookUpdateTimers = new Dictionary<OrderBookUpdateEventArgs, Timer>();
        private Dictionary<MarketTickEventArgs, Timer> marketTickTimers = new Dictionary<MarketTickEventArgs, Timer>();

        public event EventHandler<ExecutionEventArgs> ExecutionCreated;
        public event EventHandler<OrderBookSnapshotEventArgs> OrderBookSnapshot;
        public event EventHandler<OrderBookUpdateEventArgs> OrderBookUpdated;
        public event EventHandler<MarketTickEventArgs> TickerUpdated;

        public BackTestReplayer(string filePath)
        {
            this.filePath = filePath;
        }

        public void Replay()
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                var backTestData = Serializer.Deserialize<BackTestData>(fileStream);

                if (backTestData.ExecutionEvents != null)
                {
                    foreach (var kvp in backTestData.ExecutionEvents)
                    {
                        foreach (var executionEventArgs in kvp.Value)
                        {
                            executionTimers.Add(executionEventArgs, new Timer(ExecutionTimerCallback, executionEventArgs, new TimeSpan(kvp.Key), new TimeSpan(-1)));

                        }
                    }
                }

                if (backTestData.OrderBookSnapshotEvents != null)
                {
                    foreach (var kvp in backTestData.OrderBookSnapshotEvents)
                    {
                        foreach (var orderBookSnapshotEventArgs in kvp.Value)
                        {
                            var state = new OrderBookSnapshotTimerState { Tick = kvp.Key, OrderBookSnapshotEventArgs = orderBookSnapshotEventArgs };
                            orderBookSnapshotTimers.Add(kvp.Key, new Timer(OrderBookSnapshotCallback, state, new TimeSpan(kvp.Key), new TimeSpan(-1)));
                        }
                    }
                }

                if (backTestData.OrderBookUpdateEvents != null)
                {
                    foreach (var kvp in backTestData.OrderBookUpdateEvents)
                    {
                        foreach (var orderBookUpdateEventArgs in kvp.Value)
                        {
                            orderBookUpdateTimers.Add(orderBookUpdateEventArgs, new Timer(OrderBookUpdateCallback, orderBookUpdateEventArgs, new TimeSpan(kvp.Key), new TimeSpan(-1)));
                        }
                    }
                }

                if (backTestData.MarketTickEvents != null)
                {
                    foreach (var kvp in backTestData.MarketTickEvents)
                    {
                        foreach (var marketTickEventArgs in kvp.Value)
                        {
                            marketTickTimers.Add(marketTickEventArgs, new Timer(MarketTickCallback, marketTickEventArgs, new TimeSpan(kvp.Key), new TimeSpan(-1)));
                        }
                    }
                }
            }
        }

        private void ExecutionTimerCallback(object state)
        {
            var executionEventArgs = (ExecutionEventArgs)state;
            OnExecutionCreated(executionEventArgs);
            executionTimers.Remove(executionEventArgs);
        }

        private void OrderBookSnapshotCallback(object state)
        {
            var orderBookSnapshotTimerState = (OrderBookSnapshotTimerState)state;
            OnOrderBookSnapshot(orderBookSnapshotTimerState.OrderBookSnapshotEventArgs);
            orderBookSnapshotTimers.Remove(orderBookSnapshotTimerState.Tick);
        }

        private void OrderBookUpdateCallback(object state)
        {
            var orderBookUpdateEventArgs = (OrderBookUpdateEventArgs)state;
            OnOrderBookUpdated(orderBookUpdateEventArgs);
            orderBookUpdateTimers.Remove(orderBookUpdateEventArgs);
        }

        private void MarketTickCallback(object state)
        {
            var marketTickEventArgs = (MarketTickEventArgs)state;
            OnTickerUpdated(marketTickEventArgs);
            marketTickTimers.Remove(marketTickEventArgs);
        }

        protected virtual void OnExecutionCreated(ExecutionEventArgs e)
        {
            ExecutionCreated?.Invoke(this, e);
        }

        protected virtual void OnOrderBookSnapshot(OrderBookSnapshotEventArgs e)
        {
            OrderBookSnapshot?.Invoke(this, e);
        }

        protected virtual void OnOrderBookUpdated(OrderBookUpdateEventArgs e)
        {
            OrderBookUpdated?.Invoke(this, e);
        }

        protected virtual void OnTickerUpdated(MarketTickEventArgs e)
        {
            TickerUpdated?.Invoke(this, e);
        }

        private class MarketTickTimerState
        {
            public long Tick { get; set; }
            public MarketTickEventArgs MarketTickEventArgs { get; set; }
        }
    }
}
