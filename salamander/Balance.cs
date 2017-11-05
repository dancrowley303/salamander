using Newtonsoft.Json;

namespace com.defrobo.salamander
{
    public class Balance
    {
        [JsonProperty(PropertyName = "currency_code")]
        public Currency Currency { get; private set; }
        public decimal Amount { get; set; }
        public decimal Available { get; private set; }

        public Balance(Currency currency, decimal amount, decimal available)
        {
            this.Currency = currency;
            this.Amount = amount;
            this.Available = available;
        }
    }
}
