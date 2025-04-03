namespace Demeter.Api.Mqtt;

public class MqttMessageRouter
{
    private readonly IDictionary<string, IMqttMessageHandler> _handlers;

    public MqttMessageRouter(IEnumerable<(string Topic, IMqttMessageHandler Handler)> handlerMappings)
    {
        _handlers = handlerMappings.ToDictionary(x => x.Topic, x => x.Handler);
    }

    public async Task RouteAsync(string topic, string payload)
    {
        if (_handlers.TryGetValue(topic, out var handler))
        {
            await handler.HandleAsync(topic, payload);
        }
        else
        {
            throw new InvalidOperationException($"No handler found for topic: {topic}");
        }

        return;
    }
}
