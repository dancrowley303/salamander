using com.defrobo.salamander;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace salamander.backtester
{
    public class BackTestReplayer
    {
        private readonly string filePath;
        private List<Timer> timers = new List<Timer>();

        public BackTestReplayer(string filePath)
        {
            this.filePath = filePath;
        }

        public void Replay()
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                var backTestData = Serializer.Deserialize<BackTestData>(fileStream);

                foreach(var kvp in backTestData.Executions)
                {
                    timers.Add(new Timer(ExecutionTimerCallback, kvp.Value, new TimeSpan(kvp.Key), new TimeSpan(-1)));
                }
            }
        }

        private void ExecutionTimerCallback(object state)
        {
            var executions = (List<Execution>)state;

            foreach(var execution in executions)
            {
                Console.WriteLine("execution: " + execution.ID);
            }
        }
    }
}
