using FaiseStock.Data;
using FaiseStock.Data.Models;
using FaiseStock.Data.Repositories;
using FaiseStock.Jobs;
using FaiseStock.Utilities.Converters;

namespace FaiseStock.API.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminReposity _adminReposity;
        private readonly IConfigService _configService;
        private readonly IConvertCronExpression _convertCronExpression;


        public AdminService(IAdminReposity adminReposity, IConfigService configService, IConvertCronExpression convertCronExpression) {
            _adminReposity = adminReposity;
            _configService = configService;
            _convertCronExpression = convertCronExpression;
        }
        public async Task<Contest> CreateContestAsync(Contest contest)
        {
            await _adminReposity.CreateContestAsync(contest);
            _configService.UpdateGenerateRankJobSchedule(_convertCronExpression.convertToCronExpression(contest.EndDateTime));
            return contest;
        }
    }
}
