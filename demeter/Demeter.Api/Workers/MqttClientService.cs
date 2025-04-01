using System.Text;
using MQTTnet;

namespace Demeter.Api.Workers;

public class MqttClientService : BackgroundService
{
    ILogger<MqttClientService> _logger;
    IMqttClient _client;

    MqttClientOptions _clientOptions;
    MqttClientSubscribeOptions _subscriptionOptions;

    public MqttClientService(ILogger<MqttClientService> logger)
    {
        _logger = logger;

        var topic = "samples/temperature/living_room";

        var factory = new MqttClientFactory();

        _client = factory.CreateMqttClient();

        _clientOptions = new MqttClientOptionsBuilder()
            .WithTcpServer("localhost", 1883)
            .Build();

        _subscriptionOptions = factory.CreateSubscribeOptionsBuilder()
                    .WithTopicFilter(f =>
                    {
                        f.WithTopic(topic);
                        f.WithAtLeastOnceQoS();
                    })
                    .Build();

        _client.ApplicationMessageReceivedAsync += HandleMessageAsync;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _client.ConnectAsync(_clientOptions, CancellationToken.None);
        _logger.LogInformation("Connected");

        await _client.SubscribeAsync(_subscriptionOptions, CancellationToken.None);
        _logger.LogInformation("Subscribed");
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_client.IsConnected)
        {
            await _client.DisconnectAsync(new MqttClientDisconnectOptions(), cancellationToken);
        }

        await base.StopAsync(cancellationToken);
    }

    async Task HandleMessageAsync(MqttApplicationMessageReceivedEventArgs e)
    {
        var payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

        _logger.LogInformation("Received application message:");
        _logger.LogInformation($"Client: {e.ClientId}");
        _logger.LogInformation($"Topic: {e.ApplicationMessage.Topic}");
        _logger.LogInformation($"Payload: {payload}");

        await Task.CompletedTask;
    }
}