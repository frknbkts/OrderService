using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OrderService.Domain.Events;

namespace OrderService.Infrastructure.Messaging
{
    public class KafkaEventBus : IEventBus
    {
        private readonly KafkaProducer _producer;

        public KafkaEventBus(IConfiguration configuration)
        {
            _producer = new KafkaProducer(configuration);
        }

        public async Task PublishAsync<T>(string topic, string key, T @event)
        {
            await _producer.ProduceAsync(topic, key, @event);
        }
    }
} 