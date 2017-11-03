﻿using PubnubApi;
using PubnubApi.EndPoint;
using System;

namespace com.defrobo.salamander
{
    public class Ticker : ITicker
    {
        public event EventHandler<MarketTickEventArgs> Updated;
        private Pubnub pub;
        private readonly string channel;
        private SubscribeOperation<string> sub;
        private UnsubscribeOperation<string> unsub;


        public Ticker(string pubNubSubscribeKey = "sub-c-52a9ab50-291b-11e5-baaa-0619f8945a4f", string channel = "lightning_ticker_BTC_JPY")
        {
            this.channel = channel;
            pub = new Pubnub(new PNConfiguration() { SubscribeKey = pubNubSubscribeKey });
            pub.AddListener(new SubscribeCallbackExt(
                (pubnubObj, message) =>
                {
                    if (message != null)
                    {
                        OnUpdated(new MarketTickEventArgs(message.Message.ToString()));
                    }
                },
                (pubnubObj, presence) => { },
                (pubnubObj, status) => { }
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

        protected virtual void OnUpdated(MarketTickEventArgs e)
        {
            Updated?.Invoke(this, e);
        }
    }
}
