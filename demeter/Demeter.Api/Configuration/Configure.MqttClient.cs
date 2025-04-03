using Demeter.Api.Mqtt;
using Demeter.Api.Workers;

namespace Demeter.Api.Configuration;

public static partial class Configure
{
    public static WebApplicationBuilder ConfigureMqttClient(
               this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<SensorHandler>();
        
        builder.Services.AddScoped(_ => new MqttMessageRouter([
            ("device", _.GetRequiredService<SensorHandler>()),
        ]));

        builder.Services.AddHostedService<MqttClientService>();

        return builder;
    }
}

