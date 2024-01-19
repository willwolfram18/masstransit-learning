using System.Text.Json;
using MassTransit;

namespace consoleapp;

public class TestData
{
    public string Name { get; set; } = string.Empty;

    public int Value { get; set; }
}

public class TestConsumer : IConsumer<TestData>
{
    public Task Consume(ConsumeContext<TestData> context)
    {
        Console.WriteLine($"Received message {JsonSerializer.Serialize(context.Message)}");

        return Task.CompletedTask;
    }
}