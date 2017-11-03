using PubnubApi;
using PubnubApi.EndPoint;
using System;

namespace com.defrobo.salamander
{
    public class OrderBookUpdater : IOrderBookUpdater
    {
        public event EventHandler<OrderBookSnapshotEventArgs> Snapshot;
        private Pubnub pub;
        private readonly string channel;
        private SubscribeOperation<string> sub;
        private UnsubscribeOperation<string> unsub;


        public OrderBookUpdater(string pubNubSubscribeKey = "sub-c-52a9ab50-291b-11e5-baaa-0619f8945a4f", string channel = "lightning_board_BTC_JPY")
        {
            this.channel = channel;
            pub = new Pubnub(new PNConfiguration() { SubscribeKey = pubNubSubscribeKey });

            pub.AddListener(new SubscribeCallbackExt(
                (pubnubObj, message) =>
                {
                    if (message != null)
                    {
                        OnSnapshot(new OrderBookSnapshotEventArgs(message.Message.ToString()));
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
            sub = pub.Subscribe<string>().Channels(new string[] { channel });
            unsub = pub.Unsubscribe<string>().Channels(new string[] { channel });
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
            Snapshot?.Invoke(this, e);
        }
    }
}
