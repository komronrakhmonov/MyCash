
using MyCash.Domain.Enums;

namespace MyCash.Servise.DTOs;

public class WalletCreationDto
{
    public string Name { get; set; }
    public CurrencyType Currency { get; set; }
    public long UserId { get; set; }
    public decimal Amount { get; set; }
}
