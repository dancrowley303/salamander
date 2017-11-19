using PubnubApi;
using PubnubApi.EndPoint;
using System;

namespace com.defrobo.salamander
{
    public class OrderBookUpdater : IOrderBookUpdater
    {
        public event EventHandler<OrderBookSnapshotEventArgs> OrderBookSnapshot;
        public event EventHandler<OrderBookUpdateEventArgs> OrderBookUpdated;

        private const string snapshotChannel = "lightning_board_snapshot_BTC_JPY";
        private const string updateChannel = "lightning_board_BTC_JPY";
        private Pubnub pub;
        private SubscribeOperation<object> sub;
        private UnsubscribeOperation<object> unsub;


        public OrderBookUpdater(string pubNubSubscribeKey = "sub-c-52a9ab50-291b-11e5-baaa-0619f8945a4f")
        {
            pub = new Pubnub(new PNConfiguration() { SubscribeKey = pubNubSubscribeKey });

            pub.AddListener(new SubscribeCallbackExt(
                (pubnubObj, message) =>
                {
                    if (message != null)
                    {
                        if (message.Channel.Equals(updateChannel))
                        {
                            OnUpdated(new OrderBookUpdateEventArgs(message.Message.ToString()));
                        }
                        if (message.Channel.Equals(snapshotChannel))
                        {
                            OnSnapshot(new OrderBookSnapshotEventArgs(message.Message.ToString()));
                        }
                    }
                },
                (pubnubObj, presence) =>
                {
                    if (presence != null)
                    {
                        Console.WriteLine(presence.ToString());
                    }
                },
                (pubnubObj, status) =>
                {
                    if (status != null)
                    {
                        if (status.Error)
                            throw new Exception(status.ErrorData.Information);

                        Console.WriteLine("STATUS: " + status.Operation);
                    }
                }
            ));
            sub = pub.Subscribe<object>().Channels(new string[] { snapshotChannel, updateChannel });
            unsub = pub.Unsubscribe<object>().Channels(new string[] { snapshotChannel, updateChannel });
        }

        public void Start()
        {
            sub.Execute();
        }

        public void Stop()
        {
            unsub.Execute();
        }

        protected virtual void OnSnapshot(OrderBookSnapshotEventArgs e)
        {
            OrderBookSnapshot?.Invoke(this, e);
        }

        protected virtual void OnUpdated(OrderBookUpdateEventArgs e)
        {
            OrderBookUpdated?.Invoke(this, e);
        }
    }
}
