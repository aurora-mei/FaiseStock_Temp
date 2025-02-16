using FaiseStock.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FaiseStock.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FaiseStockDemoDbContext _context;
        public UserRepository(FaiseStockDemoDbContext context)
        {
            _context = context;
        }

        public async Task<Wallet> UpdateBalanceAsync(Wallet wallet)
        {
            Wallet oldWallet = _context.Wallets.Include(x=>x.user).FirstOrDefault(w => w.userId == wallet.userId)??throw new Exception("No wallet found");
            oldWallet.balance = wallet.balance;
            _context.Wallets.Update(oldWallet);
            await _context.SaveChangesAsync();
            return await _context.Wallets.FirstOrDefaultAsync(x=>x.walletId==oldWallet.walletId)??throw new Exception("No wallet found");
        }
        public async Task<ContestParticipant> AddContestParticipant(ContestParticipant contestParticipant)
        {
            Contest contest = await _context.Contests.FirstOrDefaultAsync(x=>x.contestId == contestParticipant.contestId)??throw new Exception("No contest found");
            if (contest.startDateTime < DateTime.Now)
            {
                throw new ArgumentException("Registration deadline has passed.");
            }
            await _context.AddAsync(contestParticipant);
            await _context.SaveChangesAsync();
            return await _context.ContestParticipants.Include(x=>x.user).Include(x=>x.contest).FirstOrDefaultAsync(x=>x.userId == contestParticipant.userId && x.contestId==contestParticipant.contestId);
        }
    }
}
