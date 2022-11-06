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
    public DbSet<MachineStatus> MachineStatus { get; set; }
    public DbSet<UserSupport> UserSupports { get; set; }

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MachineStatus>().HasData(
            new MachineStatus
            {
                Id = 1,
                Name = "Bom Estado",
                Created_at = DateTime.Now
            },
            new MachineStatus
            {
                Id = 2,
                Name = "Alerta",
                Created_at = DateTime.Now
            },
            new MachineStatus
            {
                Id = 3,
                Name = "Estado crítico",
                Created_at = DateTime.Now
            }
        );

        // modelBuilder.Entity<UserProblemsCategory>().HasData(
        //     new UserProblemsCategory
        //     {
        //         Id = 1,
        //         Name = "",
        //         Created_at = DateTime.Now
        //     },
        //     new UserProblemsCategory
        //     {
        //         Id = 2,
        //         Name = "",
        //         Created_at = DateTime.Now
        //     },
        //     new UserProblemsCategory
        //     {
        //         Id = 3,
        //         Name = "",
        //         Created_at = DateTime.Now
        //     },
        //     new UserProblemsCategory
        //     {
        //         Id = 4,
        //         Name = "",
        //         Created_at = DateTime.Now
        //     },
        //     new UserProblemsCategory
        //     {
        //         Id = 5,
        //         Name = "",
        //         Created_at = DateTime.Now
        //     }
        // );
        //
        // modelBuilder.Entity<MachineCategory>().HasData(
        //     new MachineCategory
        //     {
        //         
        //     }
        // );

        //modelBuilder.HasDefaultSchema("Visudata_db");
    }
}