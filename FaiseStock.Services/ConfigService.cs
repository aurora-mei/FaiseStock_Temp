using FaiseStock.API.Services.Schedule;
using FaiseStock.Jobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Quartz;

namespace FaiseStock.Configs;

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

    public async Task UpdateGenerateRankJobSchedule(string cronExpression)
    {
        var scheduler = await _schedulerFactory.GetScheduler();
        var jobKey = new JobKey("GenerateRankJob");

        // Nếu job đã tồn tại thì xóa trước
        if (await scheduler.CheckExists(jobKey))
        {
            await scheduler.DeleteJob(jobKey);
            _logger.LogInformation("Deleted existing GenerateRankJob.");
        }

        // Tạo job mới
        var job = JobBuilder.Create<GenerateRankJob>()
            .WithIdentity(jobKey)
            .Build();

        var trigger = TriggerBuilder.Create()
            .WithIdentity("GenerateRankJob-trigger")
            .WithCronSchedule(cronExpression)
            .ForJob(jobKey)
            .Build();

        // Thêm job và trigger vào scheduler
        await scheduler.ScheduleJob(job, trigger);
        _logger.LogInformation("Scheduled new GenerateRankJob with cron: {CronExpression}", cronExpression);
    }
}
    
