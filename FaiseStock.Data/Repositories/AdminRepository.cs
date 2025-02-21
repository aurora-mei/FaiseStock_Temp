using FaiseStock.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FaiseStock.Data.Repositories
{
    public class AdminRepository : IAdminReposity
    {
        private readonly FaiseStockDemoDbContext _context;
        public AdminRepository(FaiseStockDemoDbContext context)
        {
            _context = context;
        }
        public async Task<List<Contest>> GetAllContestAsync()
        {
            return await _context.Contests.ToListAsync();
        }
        public async Task<Contest> GetContestByIdAsync(string id)
        {
            Contest contest = await _context.Contests.FindAsync(id);
            if (contest == null)
            {
                throw new ArgumentNullException(nameof(contest));
            }

            return contest;
        }
        public async Task<List<ContestParticipant>> GetContestParticipantsAsync(string contestId)
        {
            return _context.ContestParticipants.Include(x=>x.contest)
                .Where(c => c.contestId == contestId).ToList();
        }
        public async Task<Contest> CreateContestAsync(Contest contest)
        {
            contest.contestId = Guid.NewGuid().ToString();
            await _context.Contests.AddAsync(contest);
            await _context.SaveChangesAsync();
            return await _context.Contests.FindAsync(contest.contestId);
        }


    }
}
