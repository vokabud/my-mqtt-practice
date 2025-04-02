using Demeter.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Demeter.Api.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<Device> Devices { get; set; }

    public DbSet<DeviceData> DeviceData { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
