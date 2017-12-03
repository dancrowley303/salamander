using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.defrobo.salamander
{
    public class CommandStatsRecorder
    {
        private readonly ConcurrentDictionary<CommandName, ConcurrentQueue<long>> executionTimes = new ConcurrentDictionary<CommandName, ConcurrentQueue<long>>();
        private readonly ConcurrentDictionary<CommandName, Stopwatch> stopwatches = new ConcurrentDictionary<CommandName, Stopwatch>();

        public void StartRecording(CommandName commandName)
        {
            Stopwatch stopwatch;
            if (stopwatches.ContainsKey(commandName))
            {
                stopwatch = stopwatches[commandName];
                stopwatch.Reset();
            }
            else
            {
                stopwatch = new Stopwatch();
                stopwatches[commandName] = stopwatch;
            }
            stopwatch.Start();
        }

        public void StopRecording(CommandName commandName)
        {
            if (!stopwatches.ContainsKey(commandName))
            {
                throw new ArgumentException("Tried to stop recording a command that was never started");
            }
            var stopwatch = stopwatches[commandName];
            stopwatch.Stop();

            Console.WriteLine(stopwatch.ElapsedMilliseconds);

            ConcurrentQueue<long> thisCommandExecutionTimes;

            if (executionTimes.ContainsKey(commandName))
            {
                thisCommandExecutionTimes = executionTimes[commandName];
            }
            else
            {
                thisCommandExecutionTimes = new ConcurrentQueue<long>();
                executionTimes[commandName] = thisCommandExecutionTimes;
            }

            thisCommandExecutionTimes.Enqueue(stopwatch.ElapsedMilliseconds);
        }

        internal double? GetMaxTime(CommandName commandName)
        {
            throw new NotImplementedException();
        }

        internal double? GetMinTime(CommandName commandName)
        {
            throw new NotImplementedException();
        }

        public double? GetAverageTime(CommandName commandName)
        {
            return executionTimes[commandName]?.Average();
        }

        public double? GetStdDev(CommandName commandName)
        {
            return executionTimes[commandName]?.StdDev();
        }

    }
}
