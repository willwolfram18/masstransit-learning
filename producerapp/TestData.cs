using System.Text.Json;
using MassTransit;

namespace producerapp;

public class TestData
{
    public string Name { get; set; } = string.Empty;

    public int Value { get; set; }
}