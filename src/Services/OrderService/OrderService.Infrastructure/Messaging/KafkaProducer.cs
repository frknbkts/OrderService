using System;
using System.Text.Json;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;

namespace OrderService.Infrastructure.Messaging
{
    public class KafkaProducer : IDisposable
    {
        private readonly IProducer<string, string> _producer;
        private bool _disposed;

        public KafkaProducer(IConfiguration configuration)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = configuration["Kafka:BootstrapServers"]
            };

            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        public async Task ProduceAsync<T>(string topic, string key, T message)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(KafkaProducer));

            var jsonMessage = JsonSerializer.Serialize(message);
            
            await _producer.ProduceAsync(topic, new Message<string, string>
            {
                Key = key,
                Value = jsonMessage
            });
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            _producer?.Dispose();
            _disposed = true;
        }
    }
} 