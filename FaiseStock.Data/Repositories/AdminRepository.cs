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
    public class AdminRepository : IAdminReposity
    {
        private readonly FaiseStockDemoDbContext _context;
        public AdminRepository(FaiseStockDemoDbContext context)
        {
            _context = context;
        }
      
     
        public async Task<Contest> CreateContestAsync(Contest contest)
        {
            contest.contestId = Guid.NewGuid().ToString();
            await _context.Contests.AddAsync(contest);
            await _context.SaveChangesAsync();
            return await _context.Contests.FindAsync(contest.contestId);
        }
        public async Task<Contest> UpdateContestAsync(Contest updateContest)
        {
            Contest contest = await  _context.Contests.FirstOrDefaultAsync(x=>x.contestId == updateContest.contestId)?? throw new Exception("No contest found");
            if(contest.startDateTime < DateTime.Now)
            {
                throw new ArgumentException("Contest has already started.");
            }
            _context.Entry(contest).CurrentValues.SetValues(updateContest);
            await _context.SaveChangesAsync();
            return await _context.Contests.FindAsync(contest.contestId);
        }

        public async Task<bool> DeleteContestAsync(string contestId)
        {
                Contest contest = await _context.Contests.FirstOrDefaultAsync(x => x.contestId == contestId)?? throw new Exception("No contest found");;
                if(contest.startDateTime < DateTime.Now)
                {
                    throw new ArgumentException("Contest has already started.");
                }
                _context.Contests.Remove(contest);
                await _context.SaveChangesAsync();
                return true;
        }
    }
}
