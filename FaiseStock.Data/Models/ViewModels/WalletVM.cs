namespace FaiseStock.Data.Models.ViewModels;

public class WalletVM
{
    public string walletId { get; set; } = null!;

    public double balance { get; set; }

    public string userName { get; set; } = null!;
}