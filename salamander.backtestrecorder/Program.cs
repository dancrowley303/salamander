using com.defrobo.salamander;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace salamander.backtester
{
    class Program
    {
        private static ExecutionAlerter executionAlerter = new ExecutionAlerter();
        private static OrderBookUpdater orderBookUpdater = new OrderBookUpdater();
        private static Ticker ticker = new Ticker();

        static void Main(string[] args)
        {
            //Record();
            Replay();
        }

        static void Replay()
        {
            var backTestReplayer = new BackTestReplayer(".\\recording.bts");
            backTestReplayer.Replay();
            Console.ReadLine();

        }

        static void Record()
        {
            var backTestRecorder = new BackTestRecorder(executionAlerter, orderBookUpdater, ticker);
            backTestRecorder.Record();
            Console.ReadLine();
            Console.WriteLine("Saving data");
            backTestRecorder.Save(".\\recording.bts");
        }
    }
}
