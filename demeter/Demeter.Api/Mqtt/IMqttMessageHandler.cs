namespace Demeter.Api.Mqtt;

public interface IMqttMessageHandler
{
    Task HandleAsync(string topic, string payload);
}

