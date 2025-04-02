using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Demeter.Api.Entities;

namespace Demeter.Api.Persistence.Configuration;

public class DeviceConfiguration : IEntityTypeConfiguration<Device>
{
    public void Configure(EntityTypeBuilder<Device> builder)
    {
        builder.HasKey(d => d.Id);

        builder
            .HasMany(d => d.Data)
            .WithOne(d => d.Device)
            .HasForeignKey(d => d.DeviceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
