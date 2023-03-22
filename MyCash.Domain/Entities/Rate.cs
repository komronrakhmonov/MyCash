using Newtonsoft.Json;

namespace MyCash.Domain.Entities;

public class Rate
{
    [JsonProperty("base")]
    public string Base { get; set; }

    [JsonProperty("rates")]
    public ExchangeRateForUSD Exchange { get; set; }
}
