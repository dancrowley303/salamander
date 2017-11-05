using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace com.defrobo.salamander
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Currency
    {
        UNKNOWN,
        JPY,
        BTC,
        BCH,
        ETH,
        ETC,
        LTC,
        MONA
    }
}
