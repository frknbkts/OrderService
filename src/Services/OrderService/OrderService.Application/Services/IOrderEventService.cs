using System.Threading.Tasks;
using OrderService.Domain.Entities;
using OrderService.Domain.Enums;

namespace OrderService.Application.Services
{
    public interface IOrderEventService
    {
        Task PublishOrderCreatedEventAsync(Order order);
        Task PublishOrderStatusChangedEventAsync(Order order, OrderStatus oldStatus);
    }
} 