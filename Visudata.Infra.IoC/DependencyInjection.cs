﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PI.Application.Intefaces;
using PI.Application.Services;
using PI.Domain.Interfaces;
using PI.Infra.Data.Context;
using PI.Infra.Data.Repositories;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace PI.Infra.IoC;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, ConfigurationManager config)
    {
        //Local connection !!!
        services.AddDbContext<VisudataDbContext>(options => options.UseMySql(
            "Server=localhost;Port=3306;Database=pi_db;User=root;Password=root;",
            ServerVersion.Parse("8.0.30-mysql" , ServerType.MySql), m => m.MigrationsAssembly(typeof(VisudataDbContext).Assembly.FullName)));

        services.AddDbContext<VisudataDbContext>();
        
        #region repositories 
        
        services.AddScoped<IEnterpriseRepository, EnterpriseRepository>();
        services.AddScoped<ILogsRepository, LogsRepository>();
        services.AddScoped<IMachineRepository, MachineRepository>();
        services.AddScoped<IOutlierRegisterRepository, OutlierRegisterRepository>();
        services.AddScoped<IMachineCategoryRepository, MachineCategoryRepository>();
        services.AddScoped<IOutlierRegisterRepository, OutlierRegisterRepository>();
        //services.AddScoped<IMachineCategoryRepository, CategoryRepository>();
        services.AddScoped<IUserSupportRepository, UserSupportRepository>();
        services.AddScoped<IEnterpriseStatusRepository, EnterpriseStatusRepository>();
        services.AddScoped<IMachineStatusRepository, MachineStatusRepository>();
        services.AddScoped<IUserProblemsCategoryRepository, UserProblemsCategoryRepository>();

        #endregion

        #region services

        services.AddScoped<IEnterpriseService, EnterpriseService>();
        services.AddScoped<IMachineService, MachineServices>();
        services.AddScoped<IUserProblemsCategoryService, UserProblemsCategoryService>();
        services.AddScoped<IUserSupportService, UserSupportService>();
        services.AddScoped<IMachineCategoryService, MachineCategoryService>();

        #endregion
    }
}