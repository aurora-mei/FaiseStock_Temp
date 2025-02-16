using System;
using System.Collections.Generic;

namespace FaiseStock.Data.Models;

public partial class TopUser
{
    public string userId { get; set; } = null!;

    public DateTime createAt { get; set; }

    public int rank { get; set; }

    public double increasedAmount { get; set; }

    public double roic { get; set; }
    public string contestId { get; set; } = null!;

    public virtual User user { get; set; } = null!;
    public virtual Contest contest { get; set; } = null!;
}
