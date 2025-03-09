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
            Contest savedContest = await _adminReposity.CreateContestAsync(contest);
            _configService.CreateLaunchContestJobSchedule(_convertCronExpression.convertToCronExpression(savedContest.startDateTime));
            _configService.CreateGenerateRankJobSchedule(_convertCronExpression.convertToCronExpression(savedContest.endDateTime));
            return contest;
        }
        public async Task<Contest> UpdateContestAsync(Contest updateContest)
        {
           Contest updatedContest =  await _adminReposity.UpdateContestAsync(updateContest);
            _configService.CreateLaunchContestJobSchedule(_convertCronExpression.convertToCronExpression(updatedContest.startDateTime));
            _configService.CreateGenerateRankJobSchedule(_convertCronExpression.convertToCronExpression(updatedContest.endDateTime));
            return updatedContest;
        }

        public async Task<bool> DeleteContestAsync(string contestId)
        {
            return await _adminReposity.DeleteContestAsync(contestId);
        }
    }
}
