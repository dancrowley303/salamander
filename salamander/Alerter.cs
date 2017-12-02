using PubnubApi;
using PubnubApi.EndPoint;
using System;

namespace com.defrobo.salamander
{
    public class Alerter : IAlerter
    {
        public event EventHandler<ExecutionEventArgs> ExecutionCreated;
        public event EventHandler<OrderBookSnapshotEventArgs> OrderBookSnapshot;
        public event EventHandler<OrderBookUpdateEventArgs> OrderBookUpdated;
        public event EventHandler<MarketTickEventArgs> TickerUpdated;

        private const string executionsChannel = "lightning_executions_BTC_JPY";
        private const string snapshotChannel = "lightning_board_snapshot_BTC_JPY";
        private const string updateChannel = "lightning_board_BTC_JPY";
        private const string tickerChannel = "lightning_ticker_BTC_JPY";

        private Pubnub pub;
        private SubscribeOperation<object> sub;
        private UnsubscribeOperation<object> unsub;

        public Alerter(string pubNubSubscribeKey = "sub-c-52a9ab50-291b-11e5-baaa-0619f8945a4f")
        {
            pub = new Pubnub(new PNConfiguration() { SubscribeKey = pubNubSubscribeKey });

            pub.AddListener(new SubscribeCallbackExt(
                (pubnubObj, message) =>
                {
                    if (message != null)
                    {
                        if (message.Channel.Equals(executionsChannel))
                        {
                            OnExecutionCreated(new ExecutionEventArgs(message.Message.ToString()));
                        }
                        if (message.Channel.Equals(updateChannel))
                        {
                            OnOrderBookUpdated(new OrderBookUpdateEventArgs(message.Message.ToString()));
                        }
                        if (message.Channel.Equals(snapshotChannel))
                        {
                            OnOrderBookSnapshot(new OrderBookSnapshotEventArgs(message.Message.ToString()));
                        }
                        if (message.Channel.Equals(tickerChannel))
                        {
                            OnTickerUpdated(new MarketTickEventArgs(message.Message.ToString()));
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

            sub = pub.Subscribe<object>().Channels(new string[] { executionsChannel, snapshotChannel, updateChannel, tickerChannel });
            unsub = pub.Unsubscribe<object>().Channels(new string[] { executionsChannel, snapshotChannel, updateChannel, tickerChannel });
        }

        public void Start()
        {
            sub.Execute();
        }

        public void Stop()
        {
            unsub.Execute();
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


    }
}
