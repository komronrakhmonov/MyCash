
using Newtonsoft.Json;

namespace MyCash.Servise.DTOs;

public class ExchangeRateDto
{

    [JsonProperty("UZS")]
    public string UZS { get; set; }

    [JsonProperty("RUB")]
    public string RUb { get; set; }

    [JsonProperty("EUR")]
    public string EUR { get; set; }
}
