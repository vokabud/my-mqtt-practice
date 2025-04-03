using System.Text.Json.Serialization;

namespace Demeter.Api.Mqtt;

internal class DeviceStatusDto
{
    [JsonPropertyName("device_id")]
    public string DeviceId { get; set; }

    [JsonPropertyName("sensor")]
    public string Sensor { get; set; }

    [JsonPropertyName("temperature")]
    public double Temperature { get; set; }

    [JsonPropertyName("unit")]
    public string Unit { get; set; }

    [JsonPropertyName("timestamp")]
    public long Timestamp { get; set; }
}

