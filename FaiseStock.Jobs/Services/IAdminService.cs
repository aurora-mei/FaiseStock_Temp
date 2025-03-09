using FaiseStock.Data.Models;

namespace FaiseStock.API.Services
{
    public interface IAdminService
    {
        public Task<Contest> CreateContestAsync(Contest contest);
        Task<Contest> UpdateContestAsync(Contest updateContest);
        Task<bool> DeleteContestAsync(string contestId);
         }
}
