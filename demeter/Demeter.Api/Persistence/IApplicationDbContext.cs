using Demeter.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Demeter.Api.Persistence;

public interface IApplicationDbContext
{
    DbSet<Device> Devices { get; set; }

    DbSet<DeviceData> DeviceData { get; set; }
}
