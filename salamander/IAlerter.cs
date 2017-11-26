using System;

namespace com.defrobo.salamander
{
    public interface IAlerter
    {
        event EventHandler<ExecutionEventArgs> ExecutionCreated;
        event EventHandler<OrderBookSnapshotEventArgs> OrderBookSnapshot;
        event EventHandler<OrderBookUpdateEventArgs> OrderBookUpdated;
        event EventHandler<MarketTickEventArgs> TickerUpdated;
        void Start();
        void Stop();
    }
}
