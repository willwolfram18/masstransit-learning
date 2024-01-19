using System.Text.Json;
using Confluent.Kafka;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace producerapp;

public class BackgroundProducer : BackgroundService
{
    private static readonly Random _rng = new Random();
    private readonly IServiceProvider _provider;

    // private readonly IBusControl _bus;
    // private readonly ITopicProducer<TestData> _producer;

    public BackgroundProducer(
        // IBusControl bus,
        // ITopicProducer<TestData> producer
        IServiceProvider provider
    )
    {
        _provider = provider;
        // _bus = bus;
        // _producer = producer;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // await _bus.StartAsync(stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            await using var scope = _provider.CreateAsyncScope();
            var _producer = scope.ServiceProvider.GetRequiredService<ITopicProducer<TestData>>();

            var data = new TestData
            {
                Name = Guid.NewGuid().ToString(),
                Value = _rng.Next(0, 100)
            };
            Console.WriteLine($"Published message is {JsonSerializer.Serialize(data)}");
            await _producer.Produce(data, stoppingToken);
            await Task.Delay(500, stoppingToken);
        }

        // await _bus.StopAsync();
    }
}