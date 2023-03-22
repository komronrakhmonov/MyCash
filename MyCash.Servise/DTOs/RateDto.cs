using Newtonsoft.Json;

namespace MyCash.Servise.DTOs;

public class RateDto
{
    [JsonProperty("base")]
    public string Base { get; set; }

    [JsonProperty("rates")]
    public ExchangeRateDto ExchangeDto { get; set; }
}
