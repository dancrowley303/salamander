using PubnubApi;
using PubnubApi.EndPoint;
using System;

namespace com.defrobo.salamander
{
    public class OrderBookUpdater : IOrderBookUpdater
    {
        public event EventHandler<OrderBookUpdateEventArgs> Snapshot;
        public event EventHandler<OrderBookUpdateEventArgs> Updated;
        private Pubnub pub;
        private readonly string channel;
        private SubscribeOperation<string> sub;
        private UnsubscribeOperation<string> unsub;


        public OrderBookUpdater(string pubNubSubscribeKey = "sub-c-52a9ab50-291b-11e5-baaa-0619f8945a4f", string channel = "lightning_board_snapshot_BTC_JPY")
        {
            this.channel = channel;
            pub = new Pubnub(new PNConfiguration() { SubscribeKey = pubNubSubscribeKey });

            pub.AddListener(new SubscribeCallbackExt(
                (pubnubObj, message) =>
                {
                    if (message != null)
                    {
                        if (message.Channel.Equals("lightning_board_BTC_JPY"))
                        {
                            OnUpdated(new OrderBookUpdateEventArgs(message.Message.ToString()));
                        }
                        if (message.Channel.Equals("lightning_board_snapshot_BTC_JPY"))
                        {
                            Console.WriteLine("*****SNAPSHOT*******");
                            OnSnapshot(new OrderBookUpdateEventArgs(message.Message.ToString()));
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
            sub = pub.Subscribe<string>().Channels(new string[] { "lightning_board_snapshot_BTC_JPY", "lightning_board_BTC_JPY" });
            unsub = pub.Unsubscribe<string>().Channels(new string[] { "lightning_board_snapshot_BTC_JPY", "lightning_board_BTC_JPY" });
        }

        public void Start()
        {
            sub.Execute();
        }

        public void Stop()
        {
            unsub.Execute();
        }

        protected virtual void OnSnapshot(OrderBookUpdateEventArgs e)
        {
            Snapshot?.Invoke(this, e);
        }

        protected virtual void OnUpdated(OrderBookUpdateEventArgs e)
        {
            Updated?.Invoke(this, e);
        }
    }
}
