using System;
using System.Collections.Generic;

namespace FaiseStock.Data.Models;

public partial class User
{
    public string userId { get; set; } = null!;

    public string name { get; set; } = null!;

    public virtual ICollection<DepositHistory> depositHistories { get; set; } = new List<DepositHistory>();

    public virtual ICollection<TopUser> topUsers { get; set; } = new List<TopUser>();

    public virtual ICollection<Wallet> wallets { get; set; } = new List<Wallet>();
    public virtual ICollection<ContestParticipant> contestParticipants { get; set; } = new List<ContestParticipant>();

}
