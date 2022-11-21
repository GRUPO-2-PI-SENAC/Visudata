using Microsoft.EntityFrameworkCore;
using PI.Domain.Entities;

namespace PI.Infra.Data.Context;

public class VisudataDbContext : DbContext
{
    public VisudataDbContext(DbContextOptions<VisudataDbContext> options) : base(options)
    {
    }

    #region DbSet

    public DbSet<Enterprise> Enterprises { get; set; }
    public DbSet<EnterpriseStatus> EnterpriseStatus { get; set; }
    public DbSet<UserProblemsCategory> UserProblemsCategories { get; set; }
    public DbSet<Machine> Machines { get; set; }
    public DbSet<OutlierRegister> OutlierRegisters { get; set; }
    public DbSet<Log> Logs { get; set; }
    public DbSet<MachineCategory> MachineCategories { get; set; }
    public DbSet<UserSupport> UserSupports { get; set; }

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<MachineCategory>().HasData(
            new MachineCategory
            {
                Created_at = DateTime.Now,
                Name = "Injetoras",
            },
            new MachineCategory
            {
                Created_at = DateTime.Now,
                Name = "Sensores"
            },
            new MachineCategory
            {
                Created_at = DateTime.Now,
                Name = "Extratoras"
            },
            new MachineCategory
            {
                Created_at = DateTime.Now,
                Name = "Empilhadeiras"
            },
            new MachineCategory
            {
                Created_at = DateTime.Now,
                Name = "Esteiras"
            },
            new MachineCategory
            {
                Created_at = DateTime.Now,
                Name = "Motores"
            }
            );

        modelBuilder.Entity<UserProblemsCategory>().HasData(
            new UserProblemsCategory
            {
                Name = "Visualizaçäo de gráfico",
                Created_at = DateTime.Now
            },
            new UserProblemsCategory
            {
                Name = "Exportaçäo de dados",
                Created_at = DateTime.Now
            },
            new UserProblemsCategory
            {
                Name = "Cadastro de máquinas",
                Created_at = DateTime.Now
            },
            new UserProblemsCategory
            {
                Name = "Ediçäo de máquinas",
                Created_at = DateTime.Now
            },
            new UserProblemsCategory
            {
                Name = "Fechamento de programas",
                Created_at = DateTime.Now
            },
            new UserProblemsCategory
            {
                Name = "Conexäo com sensores",
                Created_at = DateTime.Now
            }
        );

    }
}