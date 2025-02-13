
using FaiseStock.Data.Models;
using FaiseStock.Data.Repositories;
using FaiseStock.Data;
using FaiseStock.Utilities.Converters;
using FaiseStock.Jobs;

namespace FaiseStock.API.Services
{
    public class RankService : IRankService
    {
        private readonly IRankRepository _rankReposity;
        private readonly IConfigService _configService;
        private readonly IConvertCronExpression _convertCronExpression;


        public RankService(IRankRepository rankReposity, IConfigService configService, IConvertCronExpression convertCronExpression)
        {
            _rankReposity = rankReposity;
            _configService = configService;
            _convertCronExpression = convertCronExpression;
        }
        public async Task GenerateRankAsync()
        {
            try
            {
                string contestEndTimeStr = _configService.GetGenerateRankJobSchedule();
                DateTime contestEndTime = _convertCronExpression.convertFromCronExpression(contestEndTimeStr);
                await _rankReposity.GenerateRankAsync(contestEndTime);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
        public async Task LaunchContestAsync()
        {
            try
            {
                string contestStartTimeStr = _configService.GetLaunchContestJobSchedule();
                DateTime contestStartTime = _convertCronExpression.convertFromCronExpression(contestStartTimeStr);
                await _rankReposity.LaunchContestAsync(contestStartTime);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
    }
}
