using System;
using System.Collections.Generic;

namespace FaiseStock.Data.Models;

public partial class Wallet
{
    public string WalletId { get; set; } = null!;

    public double Balance { get; set; }

    public string UserId { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
