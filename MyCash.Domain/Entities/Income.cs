
using MyCash.Domain.Commons;
using MyCash.Domain.Enums;

namespace MyCash.Domain.Entities;

public class Income : Auditable
{
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public long CategoryId { get; set; }
    public long WalletId { get; set; }
}
