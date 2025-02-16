using System;
using System.Collections.Generic;

namespace FaiseStock.Data.Models;

public partial class DepositHistory
{
    public string depositId { get; set; } = null!;

    public string userId { get; set; } = null!;

    public double amount { get; set; }
    public DateTime createAt { get; set; }

    public virtual User user { get; set; } = null!;
}
