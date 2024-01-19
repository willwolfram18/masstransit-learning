A quick sample solution I made to try learning MassTransit library for Kafka

## Requirements
* Docker desktop installed
* .NET SDK 6.x installed

## How to run
* Execute `docker compose up -d`
* Verify broker is running by opening [http://localhost:9021/] and verifying there is a `test-topic` present
* Run the `producerapp` code to create some sample data
* Rune the `consoleapp` code to consume messages from the topic