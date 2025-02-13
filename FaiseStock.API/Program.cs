using FaiseStock.Data;
using FaiseStock.Data.Models;
using FaiseStock.Data.Repositories;
using FaiseStock.Utilities.Mapping;
using Microsoft.EntityFrameworkCore;
using Quartz.Impl;
using Quartz.Spi;
using Quartz;
using FaiseStock.Jobs;
using FaiseStock.Middlewares;
using FaiseStock.API.Services;
using FaiseStock.j;
using FaiseStock.Utilities.Converters;
using FaiseStock.Jobs.Jobs;

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
            builder.Services.AddTransient<IRankRepository, RankRepository>();
            builder.Services.AddTransient<IAdminReposity, AdminRepository>();
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IAdminService, AdminService>();
            builder.Services.AddTransient<IRankService, RankService>();
            builder.Services.AddTransient<IConfigService, ConfigService>();
            builder.Services.AddTransient<IConvertCronExpression, ConvertCronExpression>();
            


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseHttpsRedirection();

            app.UseAuthorization();
            //cors config
            app.UseCors("AllowReactApp");

            app.MapControllers();

            app.Run();
        }
    }
}
