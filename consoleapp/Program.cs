using Confluent.Kafka;
using consoleapp;
using MassTransit;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        services.AddMassTransit(transit =>
        {
            transit.UsingInMemory((context, config) => config.ConfigureEndpoints(context));
            transit.AddRider(rider =>
            {
                rider.AddConsumer<TestConsumer>();

                rider.UsingKafka((context, kafka) =>
                {
                    kafka.Host("localhost:9092");
                    kafka.TopicEndpoint<Null, TestData>("test-topic", new ConsumerConfig
                    {
                        AutoOffsetReset = AutoOffsetReset.Earliest,
                        GroupId = "example-consumer",
                    }, endpoint =>
                    {
                        endpoint.ConfigureConsumer<TestConsumer>(context);
                        endpoint.SetValueDeserializer(new JsonDeserializer<TestData>());
                        endpoint.ConcurrentMessageLimit = 10;
                        endpoint.ConcurrentDeliveryLimit = 10;
                    });
                });
            });
        });
    })
    .Build();

host.Run();
