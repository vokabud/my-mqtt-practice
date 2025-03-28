using System.Text;
using MQTTnet;

var mqttFactory = new MqttClientFactory();


using (var mqttClient = mqttFactory.CreateMqttClient())
{
    var mqttClientOptions = new MqttClientOptionsBuilder()
        .WithTcpServer("localhost", 1883)
        .Build();

    // Setup message handling before connecting so that queued messages
    // are also handled properly. When there is no event handler attached all
    // received messages get lost.
    mqttClient.ApplicationMessageReceivedAsync += e =>
    {
        var payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

        Console.WriteLine("Received application message:");

        Console.WriteLine($"Client: {e.ClientId}");
        Console.WriteLine($"Topic: {e.ApplicationMessage.Topic}");
        Console.WriteLine($"payload: {payload}");

        return Task.CompletedTask;
    };

    await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

    var mqttSubscribeOptions = mqttFactory
        .CreateSubscribeOptionsBuilder()
        .WithTopicFilter("samples/temperature/living_room")
        .Build();

    await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

    Console.WriteLine("MQTT client subscribed to topic.");

    Console.WriteLine("Press enter to exit.");
    Console.ReadLine();
}
