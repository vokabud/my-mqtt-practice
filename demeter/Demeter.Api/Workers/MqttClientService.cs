using System.Text;
using MQTTnet;
using MQTTnet.Adapter;
using MQTTnet.Exceptions;

namespace Demeter.Api.Workers;

public class MqttClientService : BackgroundService
{
    private readonly ILogger<MqttClientService> _logger;
    private readonly IMqttClient _client;

    private readonly MqttClientOptions _clientOptions;
    private readonly MqttClientSubscribeOptions _subscriptionOptions;

    public MqttClientService(ILogger<MqttClientService> logger)
    {
        _logger = logger;

        var topic = "device";

        var factory = new MqttClientFactory();

        _client = factory.CreateMqttClient();

        _clientOptions = new MqttClientOptionsBuilder()
            .WithTcpServer("localhost", 1883)
            .Build();

        _subscriptionOptions = factory
            .CreateSubscribeOptionsBuilder()
            .WithTopicFilter(f =>
            {
                f.WithTopic(topic);
                f.WithAtLeastOnceQoS();
            })
            .Build();

        _client.ApplicationMessageReceivedAsync += HandleMessageAsync;
        _client.ConnectedAsync += HandleConnectedAsync;
        _client.DisconnectedAsync += HandleDisconnectedAsync;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await _client
                .ConnectAsync(
                    _clientOptions,
                    CancellationToken.None);

            _logger.LogInformation("Connected");

            await _client
                .SubscribeAsync(
                    _subscriptionOptions,
                    CancellationToken.None);

            _logger.LogInformation("Subscribed");

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
        catch (MqttConnectingFailedException ex)
        {
            _logger.LogError(ex, "MQTT connection failed.");
        }
        catch (MqttProtocolViolationException ex)
        {
            _logger.LogError(ex, "MQTT protocol violation.");
        }
        catch (MqttCommunicationTimedOutException ex)
        {
            _logger.LogError(ex, "MQTT communication timed out.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "MQTT connection/subscription failed.");
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_client.IsConnected)
        {
            await _client.DisconnectAsync(new MqttClientDisconnectOptions(), cancellationToken);
        }

        await base.StopAsync(cancellationToken);
    }

    private Task HandleMessageAsync(MqttApplicationMessageReceivedEventArgs e)
    {
        var payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

        _logger.LogInformation("Received application message:");
        _logger.LogInformation($"Client: {e.ClientId}");
        _logger.LogInformation($"Topic: {e.ApplicationMessage.Topic}");
        _logger.LogInformation($"Payload: {payload}");

        return Task.CompletedTask;
    }

    private Task HandleConnectedAsync(MqttClientConnectedEventArgs _)
    {
        _logger.LogInformation("Connected");
        return Task.CompletedTask;
    }

    private async Task HandleDisconnectedAsync(MqttClientDisconnectedEventArgs _)
    {
        _logger.LogWarning("Disconnected. Attempting to reconnect...");

        await Task.Delay(TimeSpan.FromSeconds(5));

        try
        {
            await _client
                .ConnectAsync(
                    _clientOptions,
                    CancellationToken.None);

            await _client
                .SubscribeAsync(
                    _subscriptionOptions,
                    CancellationToken.None);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Reconnection failed.");
        }
    }
}