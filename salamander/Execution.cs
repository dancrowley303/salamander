using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace com.defrobo.salamander
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Side
    {
        Buy,
        Sell
    }
    public class Execution
    {
        [JsonProperty(PropertyName = "id")]
        public int ID { get; }

        [JsonProperty(PropertyName = "side")]
        public Side Side { get; }

        [JsonProperty(PropertyName = "price")]
        public decimal Price { get; }

        [JsonProperty(PropertyName = "size")]
        public decimal Size { get; }

        [JsonProperty(PropertyName = "exec_date")]
        public DateTime ExecutionDate { get; }

        [JsonProperty(PropertyName = "buy_child_order_acceptance_id")]
        public string BuyChildOrderAcceptanceId { get; }

        [JsonProperty(PropertyName = "sell_child_order_acceptance_id")]
        public string SellChildOrderAcceptanceId { get; }

        public Execution(int id, Side side, decimal price, decimal size, DateTime executionDate, string buyChildOrderAcceptanceId, string sellChildOrderAcceptanceId)
        {
            this.ID = id;
            this.Side = side;
            this.Price = price;
            this.Size = size;
            this.ExecutionDate = executionDate;
            this.BuyChildOrderAcceptanceId = buyChildOrderAcceptanceId;
            this.SellChildOrderAcceptanceId = sellChildOrderAcceptanceId;
        }
    }
}
