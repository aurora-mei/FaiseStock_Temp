using FaiseStock.Data.Models;
using FaiseStock.Data.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaiseStock.Data.Repositories
{
    public interface IRankRepository
    {
        Task<List<TopUser>> GetRankByDateAsync(DateOnly keydate);
        Task<List<TopUser>> GetRankAsync();
        Task<List<TopUser>> GetRankByContestAsync(String contestName);
        Task GenerateRankAsync(DateTime contestEndTime);
        Task LaunchContestAsync(DateTime contestStartTime);
        bool ClearRank();
    }
}
