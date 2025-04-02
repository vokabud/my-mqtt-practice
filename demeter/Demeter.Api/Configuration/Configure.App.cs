using Demeter.Api.Endpoints.Devices;

namespace Demeter.Api.Configuration;

public static partial class Configure
{
    public static WebApplication ConfigureApp(this WebApplicationBuilder builder)
    {
        var app = builder
            .ConfigureSwagger()
            .ConfigurePersistence()
            .ConfigureMqttClient()
            .Build();

        app.RunMigrations();

        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();

        app.MapDevicesEndpoints();

        return app;
    }
}
