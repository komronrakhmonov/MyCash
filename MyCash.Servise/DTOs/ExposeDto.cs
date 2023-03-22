namespace MyCash.Servise.DTOs;

public class ExposeDto
{
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public int CategoryId { get; set; }
    public long WalletId { get; set; }
}
