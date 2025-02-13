namespace FaiseStock.Jobs;

public interface IConfigService
{
    public string GetGenerateRankJobSchedule();
    public string GetLaunchContestJobSchedule();
    public  Task CreateGenerateRankJobSchedule(string cronExpression);
    public  Task CreateLaunchContestJobSchedule(string cronExpression);
}