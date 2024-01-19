using System.Text;
using System.Text.Json;
using Confluent.Kafka;

namespace producerapp;

public class JsonSerializer<T> : IAsyncSerializer<T?>
{
    public Task<byte[]> SerializeAsync(T? data, SerializationContext context)
    {
        var bytes = data is null ?
            Array.Empty<byte>() :
            JsonSerializer.SerializeToUtf8Bytes(data);

        return Task.FromResult(bytes);
    }
}

public class JsonDeserializer<T> : IDeserializer<T?>
{
    public T? Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        if (isNull) return default;

        return JsonSerializer.Deserialize<T>(data)!;
    }
}