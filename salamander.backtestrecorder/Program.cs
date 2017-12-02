using com.defrobo.salamander;
using System;
using System.Threading;

namespace salamander.backtestrecorder
{
    class Program
    {
        private static IAlerter alerter = new Alerter();
        private static BackTestRecorder backTestRecorder = new BackTestRecorder(alerter);
        private static string filePath = ".\\salamander-{0}.btr";
        private static Timer timer;

        static void Main(string[] args)
        {
            Console.WriteLine("Starting recorder, Hit enter to stop recording.");
            Start();
            Console.ReadLine();
            CaptureToOutput(true);
        }

        private static string DateSuffixGenerator(Boolean? lastWrite)
        {
            //return DateTime.Now.ToString("yyyy-MM-dd-HH");
            if (lastWrite.HasValue && lastWrite.Value)
            {
                return DateTime.Now.ToString("yyyy-MM-dd-HH");
            }
            else
            {
                return DateTime.Now.AddHours(-1).ToString("yyyy-MM-dd-HH");
            }
        }

        private static string MakeFilePath(Boolean? lastWrite)
        {
            return string.Format(filePath, DateSuffixGenerator(lastWrite));
        }

        private static void Start()
        {
            DateTime now = DateTime.Now;

            timer = new Timer(CaptureToOutput, filePath, TimeSpan.FromSeconds(3600 - (now.Minute * 60 + now.Second)), TimeSpan.FromHours(1));
            backTestRecorder.Record();
        }

        private static void Stop()
        {
            backTestRecorder.Stop();
            backTestRecorder.Clear();
        }

        private static void CaptureToOutput(object state)
        {
            var lastWrite = state as Boolean?;
            var fullFilePath = MakeFilePath(lastWrite);
            Console.WriteLine("Writing to {0}", fullFilePath);
            backTestRecorder.Save(fullFilePath);
            backTestRecorder.Clear();
        }
    }
}
