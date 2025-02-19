using System.Threading.Tasks;

namespace OrderService.Domain.Events
{
    public interface IEventBus
    {
        Task PublishAsync<T>(string topic, string key, T @event);
    }
} 