using FaiseStock.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FaiseStock.Data.Models.ViewModels;

namespace FaiseStock.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FaiseStockDemoDbContext _context;
        public UserRepository(FaiseStockDemoDbContext context)
        {
            _context = context;
        }

        public async Task<List<Contest>> GetAllContestAsync()
        {
            return await _context.Contests.ToListAsync();
        }
        public async Task<Contest> GetContestByIdAsync(string id)
        {
            Contest contest = await _context.Contests.FirstOrDefaultAsync(x => x.contestId == id) ?? throw new Exception("No contest found"); ;
            if (contest == null)
            {
                throw new ArgumentNullException(nameof(contest));
            }

            return contest;
        }
        public async Task<List<ContestParticipant>> GetContestParticipantsAsync(string contestId)
        {
            return await _context.ContestParticipants.Include(x => x.contest)
                .Where(c => c.contestId == contestId).ToListAsync();
        }
        public async Task<ContestParticipant> AddContestParticipant(ContestParticipant contestParticipant)
        {
            Contest contest = await _context.Contests.FirstOrDefaultAsync(x=>x.contestId == contestParticipant.contestId)??throw new Exception("No contest found");
            if (contest.startDateTime < DateTime.Now)
            {
                throw new ArgumentException("Registration deadline has passed.");
            }
            ContestParticipant existedParticipant = await _context.ContestParticipants.FirstOrDefaultAsync(x => x.userId == contestParticipant.userId && x.contestId == contestParticipant.contestId);
            if (existedParticipant != null)
            {
                throw new ArgumentException("User already registered for this contest.");
            }
            await _context.ContestParticipants.AddAsync(contestParticipant);
            await _context.SaveChangesAsync();
            return await _context.ContestParticipants.Include(x=>x.user).Include(x=>x.contest).FirstOrDefaultAsync(x=>x.userId == contestParticipant.userId && x.contestId==contestParticipant.contestId);
        }
    }
}
