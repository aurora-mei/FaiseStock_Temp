using System;
using System.Collections.Generic;

namespace FaiseStock.Data.Models;

public partial class TopUser
{
    public string UserId { get; set; } = null!;

    public DateTime CreateAt { get; set; }

    public int Rank { get; set; }

    public double IncreasedAmount { get; set; }

    public double Roic { get; set; }

    public virtual User User { get; set; } = null!;
}
