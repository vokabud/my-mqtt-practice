using MQTTnet;

var mqttFactory = new MqttClientFactory();

using (var mqttClient = mqttFactory.CreateMqttClient())
{
    var mqttClientOptions = new MqttClientOptionsBuilder()
        .WithTcpServer("localhost", 1883)
        .Build();

    await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

    var applicationMessage = new MqttApplicationMessageBuilder()
        .WithTopic("samples/temperature/living_room")
        .WithPayload("19.5")
        .Build();

    await mqttClient.PublishAsync(applicationMessage, CancellationToken.None);

    applicationMessage = new MqttApplicationMessageBuilder()
        .WithTopic("samples/temperature/living_room")
        .WithPayload("20.0")
        .Build();

    await mqttClient.PublishAsync(applicationMessage, CancellationToken.None);

    applicationMessage = new MqttApplicationMessageBuilder()
        .WithTopic("samples/temperature/living_room")
        .WithPayload("21.0")
        .Build();

    await mqttClient.PublishAsync(applicationMessage, CancellationToken.None);

    await mqttClient.DisconnectAsync();

    Console.WriteLine("MQTT application message is published.");
}
