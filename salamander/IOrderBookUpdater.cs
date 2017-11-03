using System;

namespace com.defrobo.salamander
{
    public interface IOrderBookUpdater
    {
        event EventHandler<OrderBookSnapshotEventArgs> Snapshot;
        //event EventHandler<OrderBookUpdatedEventArgs> Updated;
    }
}
