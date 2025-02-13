namespace FaiseStock.Jobs;

public interface IConfigService
{
    public string GetGenerateRankJobSchedule();
    public  Task UpdateGenerateRankJobSchedule(string newCronExpression);
}