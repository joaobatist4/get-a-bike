using GetABike.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GetABike.Infra.Context;

public class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Bike> Bikes { get; set; }
    public DbSet<Lease> Leases { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
}