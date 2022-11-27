using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PI.Application.AppServices;
using PI.Application.Intefaces;
using PI.Application.Services;
using PI.Domain.Interfaces.Repositories;
using PI.Infra.Data.Context;
using PI.Infra.Data.Repositories;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace PI.Infra.IoC;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, ConfigurationManager config)
    {
        #region Connectiondb

        services.AddDbContext<VisudataDbContext>(options => options.UseMySql(
            "Server=recnplay-server.mysql.database.azure.com;UserID = davifmelo;Password=password13258046A@;Database=visudata_db;;",
            ServerVersion.Parse("8.0.30-mysql", ServerType.MySql), m => m.MigrationsAssembly(typeof(VisudataDbContext).Assembly.FullName)));
        services.AddDbContext<VisudataDbContext>();

        #endregion

        #region repositories 

        services.AddScoped<IEnterpriseRepository, EnterpriseRepository>();
        services.AddScoped<ILogsRepository, LogsRepository>();
        services.AddScoped<IMachineRepository, MachineRepository>();
        services.AddScoped<IOutlierRegisterRepository, OutlierRegisterRepository>();
        services.AddScoped<IMachineCategoryRepository, MachineCategoryRepository>();
        services.AddScoped<IOutlierRegisterRepository, OutlierRegisterRepository>();
        services.AddScoped<IUserSupportRepository, UserSupportRepository>();
        services.AddScoped<IEnterpriseStatusRepository, EnterpriseStatusRepository>();
        services.AddScoped<IUserProblemsCategoryRepository, UserProblemsCategoryRepository>();

        #endregion

        #region services

        services.AddScoped<IEnterpriseService, EnterpriseService>();
        services.AddScoped<IMachineService, MachineServices>();
        services.AddScoped<IUserProblemsCategoryService, UserProblemsCategoryService>();
        services.AddScoped<IUserSupportService, UserSupportService>();
        services.AddScoped<IMachineCategoryService, MachineCategoryService>();

        #endregion

        #region AppService

        services.AddScoped<IEnterpriseAppService, IEnterpriseAppService>();
        services.AddScoped<IMachineCategoryAppService, MachineCategoryAppService>();
        services.AddScoped<IMachineAppService, MachineAppService>();
        services.AddScoped<IOutlierRegisterAppService, OutlierRegisterAppService>();
        services.AddScoped<IUserProblemsCategoryAppService, UserProblemsCategoryAppService>();
        services.AddScoped<IUserSupportAppService, UserSupportAppService>();

        #endregion
    }
}