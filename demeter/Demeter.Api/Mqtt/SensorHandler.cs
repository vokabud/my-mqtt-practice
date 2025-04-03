using System.Text.Json;
using Demeter.Api.Entities;
using Demeter.Api.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Demeter.Api.Mqtt;

public class SensorHandler : IMqttMessageHandler
{
    private readonly ILogger<SensorHandler> _logger;
    private readonly IApplicationDbContext _dbContext;

    public SensorHandler(
        ILogger<SensorHandler> logger, 
        ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task HandleAsync(string topic, string payload)
    {
        _logger.LogInformation($"Topic: {topic}, Payload: {payload}");

        var data = JsonSerializer.Deserialize<DeviceStatusDto>(payload);
        Console.WriteLine();
        if (data == null)
        {
            _logger.LogError("Failed to deserialize payload");

            await Task.CompletedTask;
        }

        var device = await _dbContext
            .Devices
            .FirstOrDefaultAsync(_ => _.Name == data.DeviceId);

        if (device == null)
        {
            device = new Device
            {
                Id = Guid.NewGuid(),
                Name = data.DeviceId,
                Type = "Sensor",
            };

            await _dbContext.Devices.AddAsync(device);
        }

        var deviceData = new DeviceData
        {
            Id = Guid.NewGuid(),
            DeviceId = device.Id,
            Timestamp = DateTimeOffset.FromUnixTimeMilliseconds(data.Timestamp).DateTime.ToUniversalTime(),
            Value = data.Temperature.ToString(),
        };

        await _dbContext.DeviceData.AddAsync(deviceData);

        await _dbContext.SaveChangesAsync();
    }
}

