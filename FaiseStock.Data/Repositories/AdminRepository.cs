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

        public async Task<List<ContestParticipant>> GetContestParticipantsAsync(string contestId)
        {
            return _context.ContestParticipants.Include(x=>x.Contest)
                .Include(x=>x.User).Where(c => c.ContestId == contestId).ToList();
        }
        public async Task<Contest> CreateContestAsync(Contest contest)
        {
            contest.ContestId = Guid.NewGuid().ToString();
            await _context.Contests.AddAsync(contest);
            await _context.SaveChangesAsync();
            return await _context.Contests.FindAsync(contest.ContestId);
        }


    }
}
