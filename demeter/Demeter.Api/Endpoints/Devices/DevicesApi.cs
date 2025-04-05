using Demeter.Api.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demeter.Api.Endpoints.Devices;

public record Device(Guid Id, string Name);

public static class DevicesEndpoints
{
    public static void MapDevicesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/devices", GetDevices);
    }

    static async Task<IResult> GetDevices(
        [FromServices] IApplicationDbContext dbContext)
    {
        var devices = await dbContext.Devices
            .Select(d => new Device(d.Id, d.Name))
            .ToListAsync();

        return Results.Ok(devices);
    }
}
