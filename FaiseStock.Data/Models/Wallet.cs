using System;
using System.Collections.Generic;

namespace FaiseStock.Data.Models;

public partial class Wallet
{
    public string walletId { get; set; } = null!;

    public double balance { get; set; }

    public string userId { get; set; } = null!;

    public virtual User user { get; set; } = null!;
}
