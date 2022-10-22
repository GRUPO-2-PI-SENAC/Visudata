using Microsoft.EntityFrameworkCore;
using PI.Domain.Entities;

namespace PI.Infra.Data.Context;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        
    }
    
    public DbSet<Enterprise> Enterprises { get; set; }
    public DbSet<Machine> Machines { get; set; }
    public DbSet<OutlierRegister> OutlierRegisters { get; set; }
    public DbSet<Log> Logs { get; set; }
    public DbSet<MachineCategory> MachineCategories {get; set;}
    public DbSet<MachineStatus> MachineStatus {get; set;}
    public DbSet<UserSupport> UserSupports {get; set;}
}