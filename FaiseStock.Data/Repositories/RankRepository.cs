using FaiseStock.Data.Models;
using FaiseStock.Data.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FaiseStock.Data.Repositories

{
    public class RankRepository : IRankRepository
    {
        private readonly FaiseStockDemoDbContext _context;
        public RankRepository(FaiseStockDemoDbContext context)
        {
            _context = context;
        }

        public async Task<List<TopUser>> GetRankByDateAsync(DateOnly keydate)
        {
            return await _context.TopUsers.Include(x => x.user).ThenInclude(x=>x.depositHistories).Where(x => DateOnly.FromDateTime(x.createAt) ==keydate).OrderBy(x => x.rank).ToListAsync();
        }
        public async Task<List<TopUser>> GetRankAsync()
        {
            return await _context.TopUsers.Include(x=>x.user).OrderBy(x => x.rank).ToListAsync();
        }
        public async  Task<List<TopUser>> GetRankByContestAsync(String contestId)
        {
            return await _context.TopUsers.Include(x=>x.user).Include(x=>x.contest).Where(x=>x.contestId==contestId).OrderBy(x => x.rank).ToListAsync();
        }
      
        public async Task GenerateRankAsync(DateTime contestEndTime)
        {
            Contest contest = await _context.Contests.FirstOrDefaultAsync(x =>x.endDateTime == contestEndTime)??throw new Exception("No contest found");

            //take participants & update final balance
            List<ContestParticipant> participants = _context.ContestParticipants.Where(x=>x.contestId == contest.contestId).ToList();
            foreach (ContestParticipant participant in participants)
            {
                await UpdateFinalBinance(participant);
            }
        
            //calculate increased amount and ROIC of each user
            var result = new List<(string UserId, double IncreasedAmount, double ROIC)>();
        
            foreach (var u in participants)
            {
                double increasedAmount = CalculateIncreasedAmount(u.finalBalance??0, u.initialBalance);
                double ROIC = CalculateROIC(u.finalBalance??0, increasedAmount);
                result.Add((u.userId, increasedAmount, ROIC));
            }
        
            //add top 10 user to top_user table
            var topUsers = result.OrderByDescending(x => x.IncreasedAmount)
                       .Take(10)
                       .ToList();
        
            int i = 1;
            foreach (var item in topUsers)
            {
              await AddTopUser(contest.contestId,item.UserId, item.IncreasedAmount, item.ROIC, i++);
            }
        }

        public async Task LaunchContestAsync(DateTime contestStartTime)
        {
            Contest contest = await _context.Contests.Include(x=>x.contestParticipants).FirstOrDefaultAsync(x =>x.startDateTime == contestStartTime)??throw new Exception("No contest found");
            foreach (var u in contest.contestParticipants)
            {
                await UpdateInitialBinance(u);
            }
        }
        public async Task UpdateInitialBinance(ContestParticipant contestParticipant)
        {
            User u = await _context.Users.Include(x=>x.wallets).FirstOrDefaultAsync(x=>x.userId == contestParticipant.userId)??throw new Exception("No user found");
            contestParticipant.initialBalance = u.wallets.First().balance;
            _context.Update(contestParticipant);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateFinalBinance(ContestParticipant contestParticipant)
        {
            User u = await _context.Users.Include(x=>x.wallets).FirstOrDefaultAsync(x=>x.userId == contestParticipant.userId)??throw new Exception("No user found");
            contestParticipant.finalBalance = u.wallets.First().balance;
            _context.Update(contestParticipant);
            await _context.SaveChangesAsync();
        }
  
        public double CalculateIncreasedAmount(double finalBalance, double initialBalance)
        {
            return finalBalance - initialBalance;
        }
        public double CalculateROIC(double balance, double increasedAmount)
        {
            return Math.Round(increasedAmount / balance * 100, 2);
        }

        public async Task AddTopUser(string contest_id,string user_id, double increasedAmount, double ROIC, int rank)
        {
            var topUser = new TopUser()
            {
                userId = user_id,
                rank = rank,
                increasedAmount = increasedAmount,
                roic = ROIC,
                createAt = DateTime.Now,
                contestId = contest_id
            };
            await _context.TopUsers.AddAsync(topUser);
            await _context.SaveChangesAsync();

        }

        public bool ClearRank()
        {
            return _context.Database.ExecuteSqlRaw("DELETE FROM top_user") > 0;
        }
    }
}
