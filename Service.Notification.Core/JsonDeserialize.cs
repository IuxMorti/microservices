using System.Text.Json;
using Confluent.Kafka;

namespace Service.Notification.Core;

public class JsonDeserialize<T>  : IDeserializer<T>
{
    public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        return JsonSerializer.Deserialize<T>(data)!;
    }
}