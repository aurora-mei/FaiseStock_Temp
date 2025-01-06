using FaiseStock.API.Services.Schedule;
using FaiseStock.Data.Repositories;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaiseStock.Jobs
{
    public class DemoJob : IJob
    {
        private readonly ILogger<GenerateRankJob> _logger;
        public DemoJob(IUserRepository userRepository, ILogger<GenerateRankJob> logger)
        {
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Demo Job is running");
            return Task.CompletedTask;
        }
      
    }
}
