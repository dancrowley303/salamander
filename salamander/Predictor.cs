using System.Linq;

namespace com.defrobo.salamander
{
    public class Predictor
    {
        public enum PriceDirectionCategory
        {
            Flat,
            Up,
            Down
        }

        private readonly IAlerter alerter;

        public PriceDirectionCategory PriceDirection { get; private set; }

        private decimal lastPrice;

        public Predictor(IAlerter alerter)
        {
            this.alerter = alerter;
            alerter.ExecutionCreated += Alerter_ExecutionCreated;
            alerter.OrderBookSnapshot += Alerter_OrderBookSnapshot;
            alerter.OrderBookUpdated += Alerter_OrderBookUpdated;
            alerter.TickerUpdated += Alerter_TickerUpdated;
        }

        private void Alerter_TickerUpdated(object sender, MarketTickEventArgs e)
        {
        }

        private void Alerter_OrderBookUpdated(object sender, OrderBookUpdateEventArgs e)
        {
        }

        private void Alerter_OrderBookSnapshot(object sender, OrderBookSnapshotEventArgs e)
        {
        }

        private void Alerter_ExecutionCreated(object sender, ExecutionEventArgs e)
        {
            var thisPrice = e.Executions.Aggregate((agg, next) => next.Price > agg.Price ? next : agg).Price;

            if (thisPrice < lastPrice)
            {
                PriceDirection = PriceDirectionCategory.Down;
            }
            else if (thisPrice == lastPrice)
            {
                PriceDirection = PriceDirectionCategory.Flat;
            }
            else
            {
                PriceDirection = PriceDirectionCategory.Up;
            }
            lastPrice = thisPrice;
        }
    }
}
