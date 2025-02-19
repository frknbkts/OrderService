using System.Linq;
using System.Threading.Tasks;
using OrderService.Domain.Entities;
using OrderService.Domain.Events;
using OrderService.Domain.Enums;

namespace OrderService.Application.Services
{
    public class OrderEventService : IOrderEventService
    {
        private readonly IEventBus _eventBus;
        private const string OrdersTopic = "orders";

        public OrderEventService(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task PublishOrderCreatedEventAsync(Order order)
        {
            var @event = new OrderCreatedEvent(
                order.Id,
                order.OrderNumber,
                order.UserId,
                order.TotalAmount,
                order.OrderItems.Select(item => new OrderItemEvent
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    TotalPrice = item.TotalPrice
                }).ToList(),
                new AddressEvent
                {
                    Street = order.ShippingAddress.Street,
                    City = order.ShippingAddress.City,
                    State = order.ShippingAddress.State,
                    Country = order.ShippingAddress.Country,
                    ZipCode = order.ShippingAddress.ZipCode
                },
                order.OrderDate);

            await _eventBus.PublishAsync(OrdersTopic, order.Id.ToString(), @event);
        }

        public async Task PublishOrderStatusChangedEventAsync(Order order, OrderStatus oldStatus)
        {
            var @event = new OrderStatusChangedEvent(
                order.Id,
                order.OrderNumber,
                order.UserId,
                oldStatus,
                order.Status,
                System.DateTime.UtcNow);

            await _eventBus.PublishAsync(OrdersTopic, order.Id.ToString(), @event);
        }
    }
} 