using FaiseStock.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaiseStock.Data.Repositories
{
    public interface IUserRepository
    {
        public Task<ContestParticipant> AddContestParticipant(ContestParticipant contestParticipant);
        public Task<Wallet> UpdateBalanceAsync(Wallet wallet);
    }
}
