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
    public class UserRepository : IUserRepository
    {
        private readonly FaiseStockDemoDbContext _context;
        public UserRepository(FaiseStockDemoDbContext context)
        {
            _context = context;
        }
        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<List<TopUser>> GetRankAsync(DateOnly keydate)
        {
            return await _context.TopUsers.Include(x => x.User).Where(x => x.CreateAt.Day.Equals(keydate.Day)).OrderBy(x => x.Rank).ToListAsync();
        }
        public async Task<List<TopUser>> GetRankAsync()
        {
            return await _context.TopUsers.Include(x=>x.User).OrderBy(x => x.Rank).ToListAsync();
        }
        public async Task GenerateRankAsync()
        {
            //ClearRank();
            var list = await _context.Wallets.Include(x => x.User).ThenInclude(x => x.DepositHistories).ToListAsync();

            var result = new List<(string UserId, double IncreasedAmount, double ROIC)>();

            foreach (var u in list)
            {
                double totalDeposit = await CalculateTotalDeposit(u.UserId);
                double increasedAmount = CalculateIncreasedAmount(u.Balance, totalDeposit);
                double ROIC = CalculateROIC(u.Balance, increasedAmount);

                result.Add((u.UserId, increasedAmount, ROIC));
            }
            var topUsers = result.OrderByDescending(x => x.IncreasedAmount)
                       .Take(10)
                       .ToList();

            int i = 1;
            foreach (var item in topUsers)
            {
              await AddTopUser(item.UserId, item.IncreasedAmount, item.ROIC, i++);
            }
        }


        public async Task<double> CalculateTotalDeposit(string user_id)
        {
            var user = await _context.Users.Include(x=>x.DepositHistories).FirstOrDefaultAsync(x => x.UserId == user_id);
            var sum = user.DepositHistories.Sum(x => x.Amount);
            return sum;
        }
        public double CalculateIncreasedAmount(double balance, double totalDeposit)
        {
            return balance - totalDeposit;
        }
        public double CalculateROIC(double balance, double increasedAmount)
        {
            return increasedAmount / balance * 100;
        }
        public async Task AddTopUser(string user_id, double increasedAmount, double ROIC, int rank)
        {
            var topUser = new TopUser()
            {
                UserId = user_id,
                Rank = rank,
                IncreasedAmount = increasedAmount,
                Roic = ROIC,
                CreateAt = DateOnly.FromDateTime(DateTime.Now)
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
