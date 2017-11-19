using System;

namespace com.defrobo.salamander
{
    public interface IOrderBookUpdater
    {
        event EventHandler<OrderBookSnapshotEventArgs> OrderBookSnapshot;
        event EventHandler<OrderBookUpdateEventArgs> OrderBookUpdated;
    }
}
