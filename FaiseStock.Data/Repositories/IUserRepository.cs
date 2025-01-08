using FaiseStock.Data.Models;
using FaiseStock.Data.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaiseStock.Data.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<List<TopUser>> GetRankAsync(DateOnly keydate);
        Task<List<TopUser>> GetRankAsync();
        Task GenerateRankAsync();
        Task<double> CalculateTotalDeposit(string user_id);
        bool ClearRank();
    }
}
