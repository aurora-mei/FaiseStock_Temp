
using FaiseStock.Data.Models;
using FaiseStock.Data.Repositories;
using FaiseStock.Data;
using FaiseStock.Utilities.Converters;

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
            finally
            {
                _configService.UpdateGenerateRankJobSchedule("0 0 0 13 2 ? 2025");
            }
            
        }
    }
}
