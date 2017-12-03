using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace com.defrobo.salamander
{
    public class BackTestCommandService : ICommandService
    {
        private static readonly Random random = new Random(DateTime.Now.Millisecond);
        private CommandStatsRecorder commandStatsRecorder = new CommandStatsRecorder();

        public Dictionary<Currency, Balance> Balances { get; private set; }

        public BackTestCommandService()
        {
            Balances = new Dictionary<Currency, Balance>
            {
                [Currency.JPY] = new Balance(Currency.JPY, 0, 0),
                [Currency.BTC] = new Balance(Currency.BTC, 0, 0)
            };
        }

        public double? GetAverageTime(CommandName commandName)
        {
            return commandStatsRecorder.GetAverageTime(commandName);
        }

        public double? GetStdDevTime(CommandName commandName)
        {
            return commandStatsRecorder.GetStdDev(commandName);
        }

        public double? GetMinTime(CommandName commandName)
        {
            return commandStatsRecorder.GetMinTime(commandName);
        }

        public double? GetMaxTime(CommandName commandName)
        {
            return commandStatsRecorder.GetMaxTime(commandName);
        }

        private void SimulateNetworkDelay()
        {
            var delay = random.Next(500) * random.NextDouble();
            Thread.Sleep((int)delay);
        }

        public Task<Dictionary<Currency, Balance>> GetBalances()
        {
            return Task.Run(() =>
            {
                commandStatsRecorder.StartRecording(CommandName.GetBalances);

                SimulateNetworkDelay();

                commandStatsRecorder.StopRecording(CommandName.GetBalances);

                return Balances;
            });
        }
    }
}
