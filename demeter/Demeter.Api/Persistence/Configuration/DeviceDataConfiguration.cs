using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Demeter.Api.Entities;

namespace Demeter.Api.Persistence.Configuration;

public class DeviceDataConfiguration : IEntityTypeConfiguration<DeviceData>
{
    public void Configure(EntityTypeBuilder<DeviceData> builder)
    {
        builder.HasKey(d => d.Id);
    }
}
