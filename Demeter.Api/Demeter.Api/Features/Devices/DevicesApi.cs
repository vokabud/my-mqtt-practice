namespace Demeter.Api.Features.Devices;

public record Device(int Id, string Name);

public static class DevicesEndpoints
{
    public static void MapDevicesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/devices", GetDevices);
    }

    static IResult GetDevices() => new Random().Next(0, 2) switch
    {
        0 => Results.Ok(new[] { new Device (1, "Device 1" ) }),
        1 => Results.Ok(new[] { new Device (2, "Device 2" ) }),
        _ => Results.NotFound()
    };
}
