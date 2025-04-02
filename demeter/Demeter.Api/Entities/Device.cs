namespace Demeter.Api.Entities;

public class Device
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Type { get; set; }

    public virtual ICollection<DeviceData> Data { get; set; } = [];
}
