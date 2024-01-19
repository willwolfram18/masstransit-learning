using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using producerapp;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        services.AddMassTransit(transit =>
        {
            transit.UsingInMemory((context, config) => config.ConfigureEndpoints(context));
            transit.AddRider(rider =>
            {
                rider.AddProducer<TestData>("test-topic", (x, y) => y.SetValueSerializer(new JsonSerializer<TestData>()));
                rider.UsingKafka((context, kafka) =>
                {
                    kafka.Host("localhost:9092");
                });
            });
        });
        services.AddHostedService<BackgroundProducer>();
    })
    .Build();

host.Run();
