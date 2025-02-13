using System.Text.Json;
using FaiseStock.Jobs;
using FaiseStock.Jobs.Jobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Quartz;

namespace FaiseStock.j;

public class ConfigService: IConfigService
{
    private readonly IConfiguration _configuration;
    private readonly string _configPath;
    private readonly ISchedulerFactory _schedulerFactory;
    private readonly ILogger<ConfigService> _logger;

    public ConfigService(IConfiguration configuration,ISchedulerFactory schedulerFactory, ILogger<ConfigService> logger)
    {
        _configuration = configuration;
        _configPath = "appsettings.json"; // Đường dẫn file cấu hình
        _schedulerFactory = schedulerFactory;
        _logger = logger;
    }
    
    public string GetGenerateRankJobSchedule()
    {
        return _configuration["TimeInterval:GenerateRankJob"]??"";
    }
    public string GetLaunchContestJobSchedule()
    {
        return _configuration["TimeInterval:LaunchContestJob"]??"";
    }
    private void UpdateScheduleValue(string newCronExpression,String jobName)
    {
        var json = File.ReadAllText(_configPath);
        var jsonDoc = JsonDocument.Parse(json);
        var jsonObj = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonDoc.RootElement.GetRawText());

        if (jsonObj != null && jsonObj.ContainsKey("TimeInterval"))
        {
            var timeInterval = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonObj["TimeInterval"].ToString());
            timeInterval[jobName] = newCronExpression;
            jsonObj["TimeInterval"] = timeInterval;

            var newJson = JsonSerializer.Serialize(jsonObj, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_configPath, newJson);
        }
    }
    public async Task CreateGenerateRankJobSchedule(string cronExpression)
    {
        var scheduler = await _schedulerFactory.GetScheduler();
        var jobKey = new JobKey("GenerateRankJob");
        
        // delete the existed job
        if (await scheduler.CheckExists(jobKey))
        {
            await scheduler.DeleteJob(jobKey);
            _logger.LogInformation("Deleted existing GenerateRankJob.");
        }

        // create new job
        var job = JobBuilder.Create<GenerateRankJob>()
            .WithIdentity(jobKey)
            .Build();

        var trigger = TriggerBuilder.Create()
            .WithIdentity("GenerateRankJob-trigger")
            .WithCronSchedule(cronExpression)
            .ForJob(jobKey)
            .Build();

        // add job and trigger to scheduler
        await scheduler.ScheduleJob(job, trigger);
        UpdateScheduleValue(cronExpression,"GenerateRankJob");
        _logger.LogInformation("Scheduled new GenerateRankJob with cron: {CronExpression}", cronExpression);
    }
  
    public async Task CreateLaunchContestJobSchedule(string cronExpression)
    {
        var scheduler = await _schedulerFactory.GetScheduler();
        var jobKey = new JobKey("LaunchContestJob");
        
        // delete the existed job
        if (await scheduler.CheckExists(jobKey))
        {
            await scheduler.DeleteJob(jobKey);
            _logger.LogInformation("Deleted existing LaunchContestJob.");
        }

        // create new job
        var job = JobBuilder.Create<LaunchContestJob>()
            .WithIdentity(jobKey)
            .Build();

        var trigger = TriggerBuilder.Create()
            .WithIdentity("LaunchContestJob-trigger")
            .WithCronSchedule(cronExpression)
            .ForJob(jobKey)
            .Build();

        // add job and trigger to scheduler
        await scheduler.ScheduleJob(job, trigger);
        UpdateScheduleValue(cronExpression,"LaunchContestJob");
        _logger.LogInformation("Scheduled new LaunchContestJob with cron: {CronExpression}", cronExpression);

    }
}
    
