using System;
using System.Collections.Generic;

namespace FaiseStock.Data.Models;

public partial class DepositHistory
{
    public string DepositId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public double Amount { get; set; }

    public virtual User User { get; set; } = null!;
}
