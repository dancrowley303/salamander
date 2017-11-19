using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace com.defrobo.salamander
{
    public class BackTestReplayer : IExecutionAlerter, ITicker, IOrderBookUpdater
    {

        //todo: wire timer callbacks to event handlers
        public event EventHandler<ExecutionEventArgs> ExecutionCreated;
        public event EventHandler<OrderBookSnapshotEventArgs> OrderBookSnapshot;
        public event EventHandler<OrderBookUpdateEventArgs> OrderBookUpdated;
        public event EventHandler<MarketTickEventArgs> TickerUpdated;

        private Dictionary<long, string> rawExecutionMessages  = new Dictionary<long, string>();
        private Dictionary<long, string> rawOrderBookSnapshotMessages = new Dictionary<long, string>();
        private Dictionary<long, string> rawOrderBookUpdateMesages = new Dictionary<long, string>();
        private Dictionary<long, string> rawTickerUpdateMessages = new Dictionary<long, string>();

        private List<Timer> timers = new List<Timer>();

        public BackTestReplayer(string executionsFileLocation, string orderBookSnapshotFileLocation, string orderBookUpdateFileLocation, string tickerFileLocation)
        {
            SetupReplays(executionsFileLocation, rawExecutionMessages);
            SetupReplays(orderBookSnapshotFileLocation, rawOrderBookSnapshotMessages);
            SetupReplays(orderBookUpdateFileLocation, rawOrderBookUpdateMesages);
            SetupReplays(tickerFileLocation, rawTickerUpdateMessages);

            Console.ReadLine();
        }

        private void SetupReplays(string fileLocation, Dictionary<long, string> rawMessages)
        {
            DateTime startTime;

            using (var filestream = new FileStream(fileLocation, FileMode.Open))
            {
                var baseTimeBytes = new byte[8];
                filestream.Read(baseTimeBytes, 0, 8);
                var baseTime = BitConverter.ToInt64(baseTimeBytes, 0);
                //this is not really used anymore
                startTime = new DateTime(baseTime);
                while (filestream.Position < filestream.Length)
                {
                    var tickDiffBytes = new byte[8];
                    filestream.Read(tickDiffBytes, 0, 8);
                    var tickDiff = BitConverter.ToInt64(tickDiffBytes, 0);
                    var messageSizeBytes = new byte[4];
                    filestream.Read(messageSizeBytes, 0, 4);
                    var messageSize = BitConverter.ToInt32(messageSizeBytes, 0);
                    var messageBytes = new byte[messageSize];
                    filestream.Read(messageBytes, 0, messageSize);
                    //TODO: seems there are multiple messages on same time, need to reconsider if using ticks as hash key is suitable
                    rawMessages.Add(tickDiff, Encoding.ASCII.GetString(messageBytes));
                }
            }

            foreach (var kvp in rawMessages)
            {
                timers.Add(new Timer(TimerCallback, kvp.Value, new TimeSpan(kvp.Key), new TimeSpan(-1)));
            }

        }

        private void TimerCallback(object state)
        {
            Console.WriteLine((string)state);
        }

    }
}
