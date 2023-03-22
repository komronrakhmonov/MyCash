using MyCash.Domain.Commons;
using Newtonsoft.Json;

namespace MyCash.Domain.Entities;

public class ExchangeRateForUSD : Auditable
{
    public string UZS { get; set; }
    public string RUB { get; set; }
    public string EUR { get; set; }

}
