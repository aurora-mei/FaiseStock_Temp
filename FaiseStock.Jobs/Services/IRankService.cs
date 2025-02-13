namespace FaiseStock.API.Services
{
    public interface IRankService
    {
        Task GenerateRankAsync();
        Task LaunchContestAsync();
    }
}
