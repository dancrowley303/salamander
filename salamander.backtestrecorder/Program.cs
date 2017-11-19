using com.defrobo.salamander;
using System;
using System.IO;
using System.Text;

namespace salamander.backtester
{
    class Program
    {
        private static DateTime startTime;

        private static string executionsFileLocation;
        private static string orderBookFileLocation;
        private static string orderBookSnapshotFileLocation;
        private static string tickerFileLocation;

        private static FileStream executionsFileStream;
        private static FileStream orderBookFileStream;
        private static FileStream orderBookSnapshotFileStream;
        private static FileStream tickerFileStream;

        private static bool writing = true;

        static void Main(string[] args)
        {
            Replay();
            //Record();
        }

        static void Replay()
        {
            var exFile = ".\\backtest-executions-0.txt";
            var obsFile = ".\\backtest-orderbook-snapshot-0.txt";
            var obFile = ".\\backtest-orderbook-0.txt";
            var tkFile = ".\\backtest-ticker-0.txt";
            var replayer = new BackTestReplayer(exFile, obsFile, obFile, tkFile);
        }

        static void Record()
        {
            startTime = DateTime.Now;
            var tickTimeStart = startTime.Ticks;
            var tickTimeStartBytes = BitConverter.GetBytes(tickTimeStart);
            var nowString = startTime.ToString("yyyyMMddHHmmss");

            //hack because changing file locs everytime is annoying during debugging
            nowString = "0";

            executionsFileLocation = String.Format(".\\backtest-executions-{0}.txt", nowString);
            executionsFileStream = File.Open(executionsFileLocation, FileMode.CreateNew);
            executionsFileStream.Write(tickTimeStartBytes, 0, tickTimeStartBytes.Length);

            orderBookFileLocation = String.Format(".\\backtest-orderbook-{0}.txt", nowString);
            orderBookFileStream = File.Open(orderBookFileLocation, FileMode.CreateNew);
            orderBookFileStream.Write(tickTimeStartBytes, 0, tickTimeStartBytes.Length);

            orderBookSnapshotFileLocation = String.Format(".\\backtest-orderbook-snapshot-{0}.txt", nowString);
            orderBookSnapshotFileStream = File.Open(orderBookSnapshotFileLocation, FileMode.CreateNew);
            orderBookSnapshotFileStream.Write(tickTimeStartBytes, 0, tickTimeStartBytes.Length);

            tickerFileLocation = String.Format(".\\backtest-ticker-{0}.txt", nowString);
            tickerFileStream = File.Open(tickerFileLocation, FileMode.CreateNew);
            tickerFileStream.Write(tickTimeStartBytes, 0, tickTimeStartBytes.Length);

            Console.WriteLine("Start recording executions to {0}", executionsFileLocation);
            var executionAlerter = new ExecutionAlerter();
            executionAlerter.ExecutionCreated += ExecutionAlerter_Created;
            executionAlerter.Start();

            Console.WriteLine("Start recording order book to {0}", orderBookFileLocation);
            var orderBookUpdater = new OrderBookUpdater();
            orderBookUpdater.OrderBookSnapshot += OrderBookUpdater_Snapshot;
            orderBookUpdater.OrderBookUpdated += OrderBookUpdater_Update;
            orderBookUpdater.Start();

            Console.WriteLine("Start recording ticker to {0}", tickerFileLocation);
            var ticker = new Ticker();
            ticker.TickerUpdated += Ticker_Updated;
            ticker.Start();

            Console.ReadLine();
            Console.WriteLine("Stopping recording");
            executionAlerter.Stop();
            orderBookUpdater.Stop();
            ticker.Stop();
            writing = false;

            executionsFileStream.Dispose();
            orderBookFileStream.Dispose();
            orderBookSnapshotFileStream.Dispose();
            tickerFileStream.Dispose();
        }

        private static byte[] BuildBuffer(string message)
        {
            var timeNow = DateTime.Now;
            var diff = timeNow.Subtract(startTime);
            var ticksDiff = diff.Ticks;

            var tickDiffBytes = BitConverter.GetBytes(ticksDiff);
            var messageBuffer = Encoding.ASCII.GetBytes(message);
            var messageSizeBytes = BitConverter.GetBytes(messageBuffer.Length);

            var buffer = new byte[tickDiffBytes.Length + messageSizeBytes.Length + messageBuffer.Length];
            Buffer.BlockCopy(tickDiffBytes, 0, buffer, 0, tickDiffBytes.Length);
            Buffer.BlockCopy(messageSizeBytes, 0, buffer, tickDiffBytes.Length, messageSizeBytes.Length);
            Buffer.BlockCopy(messageBuffer, 0, buffer, tickDiffBytes.Length + messageSizeBytes.Length, messageBuffer.Length);

            return buffer;
        }

        private static async void Ticker_Updated(object sender, MarketTickEventArgs e)
        {
            if (!writing) return;
            var buffer = BuildBuffer(e.RawTickMessage);
            await tickerFileStream.WriteAsync(buffer, 0, buffer.Length);
        }

        private static async void OrderBookUpdater_Update(object sender, OrderBookUpdateEventArgs e)
        {
            if (!writing) return;
            var buffer = BuildBuffer(e.RawOrderBookUpdateMessage);
            await orderBookFileStream.WriteAsync(buffer, 0, buffer.Length);
        }

        private static async void OrderBookUpdater_Snapshot(object sender, OrderBookSnapshotEventArgs e)
        {
            if (!writing) return;
            var buffer = BuildBuffer(e.RawOrderBookSnapshotMessage);
            await orderBookSnapshotFileStream.WriteAsync(buffer, 0, buffer.Length);
        }

        private static async void ExecutionAlerter_Created(object sender, ExecutionEventArgs e)
        {
            if (!writing) return;
            var buffer = BuildBuffer(e.RawExectionMessage);
            await executionsFileStream.WriteAsync(buffer, 0, buffer.Length);
        }
    }
}
