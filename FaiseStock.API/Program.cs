
using FaiseStock.API.Services.Schedule;
using FaiseStock.Data;
using FaiseStock.Data.Models;
using FaiseStock.Data.Repositories;
using FaiseStock.Utilities.Mapping;
using Microsoft.EntityFrameworkCore;
using Quartz.Impl;
using Quartz.Spi;
using Quartz;
using FaiseStock.Jobs;

namespace FaiseStock.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
            //schedule configure
            builder.Services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();

                var cronExpression = builder.Configuration.GetSection("TimeInterval:GenerateRankJob").Value;
                var jobKey = new JobKey("GenerateRankJob");
                q.AddJob<GenerateRankJob>(opts => opts.WithIdentity(jobKey));
                q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity("GenerateRankJob-trigger")
                    .WithCronSchedule(cronExpression!)
                );

                var jobKey2 = new JobKey("DemoJob");
                q.AddJob<DemoJob>(opts => opts.WithIdentity(jobKey2));
                q.AddTrigger(opts => opts
                    .ForJob(jobKey2)
                    .WithIdentity("DemoJob-trigger")
                    .WithCronSchedule(cronExpression!)
                );
            });
            builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);  // Background service for Quartz
            //cors configure
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp", builder =>
                {
                builder.WithOrigins("*")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            builder.Services.AddTransient<FaiseStockDemoDbContext>();
            builder.Services.AddTransient<IUserRepository, UserRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            //cors config
            app.UseCors("AllowReactApp");

            app.MapControllers();

            app.Run();
        }
    }
}
