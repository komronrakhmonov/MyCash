
using MyCash.Domain.Commons;
using MyCash.Domain.Enums;

namespace MyCash.Domain.Entities;

public class Expose : Auditable
{
    public string Description { get; set; }
    public decimal Amount { get; set; } 
    public long CategoryId { get; set; }
    public Category Category { get; set; }
    public long WalletId { get; set; }
    public Wallet Wallet { get; set; }

}
