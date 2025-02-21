using FaiseStock.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaiseStock.Data.Repositories
{
    public interface IAdminReposity
    {
        Task<Contest> GetContestByIdAsync(string id);
        Task<List<Contest>> GetAllContestAsync();
        Task<Contest> CreateContestAsync(Contest contest);
        Task<List<ContestParticipant>> GetContestParticipantsAsync(string contestId);
    }
}
