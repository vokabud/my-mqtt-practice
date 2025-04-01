using Demeter.Api.Workers;

namespace Demeter.Api.Configuration;

public static partial class Configure
{
    public static WebApplicationBuilder ConfigureMqttClient(
               this WebApplicationBuilder builder)
{
        builder.Services.AddHostedService<MqttClientService>();

        return builder;
    }
}

