namespace Demeter.Api.Entities;

public class DeviceData
{
    public Guid Id { get; set; }

    public DateTime Timestamp { get; set; }

    public string Value { get; set; } = string.Empty;

    public Guid DeviceId { get; set; }

    public virtual Device Device { get; set; } = null;
}
