namespace FaiseStock.Data.Models.ViewModels;

public class WalletVM
{
    public string WalletId { get; set; } = null!;

    public double Balance { get; set; }

    public string UserName { get; set; } = null!;
}