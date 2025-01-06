using System;
using System.Collections.Generic;

namespace FaiseStock.Data.Models;

public partial class User
{
    public string UserId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<DepositHistory> DepositHistories { get; set; } = new List<DepositHistory>();

    public virtual ICollection<TopUser> TopUsers { get; set; } = new List<TopUser>();

    public virtual ICollection<Wallet> Wallets { get; set; } = new List<Wallet>();
}
