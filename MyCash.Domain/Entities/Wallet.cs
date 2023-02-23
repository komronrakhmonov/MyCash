
using MyCash.Domain.Commons;
using MyCash.Domain.Enums;

namespace MyCash.Domain.Entities;

public class Wallet : Auditable
{
    public string Name { get; set; }
    public decimal Amount { get; set; } = 0;
    public CurrencyType Currency { get; set; }
    public long UserId { get; set; }
}
