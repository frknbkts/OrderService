using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OrderService.Application.Services;
using OrderService.Domain.Entities;
using OrderService.Domain.Repositories;
using OrderService.Domain.ValueObjects;

namespace OrderService.Application.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderEventService _orderEventService;

        public CreateOrderCommandHandler(
            IOrderRepository orderRepository,
            IOrderEventService orderEventService)
        {
            _orderRepository = orderRepository;
            _orderEventService = orderEventService;
        }

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var shippingAddress = Address.Create(
                request.OrderDto.ShippingAddress.Street,
                request.OrderDto.ShippingAddress.City,
                request.OrderDto.ShippingAddress.State,
                request.OrderDto.ShippingAddress.Country,
                request.OrderDto.ShippingAddress.ZipCode
            );

            var order = Order.Create(
                request.OrderDto.UserId,
                GenerateOrderNumber(),
                shippingAddress
            );

            foreach (var item in request.OrderDto.OrderItems)
            {
                order.AddOrderItem(
                    item.ProductId,
                    item.ProductName,
                    item.UnitPrice,
                    item.Quantity
                );
            }

            await _orderRepository.AddAsync(order);
            await _orderEventService.PublishOrderCreatedEventAsync(order);

            return order.Id;
        }

        private string GenerateOrderNumber()
        {
            return $"ORD-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N").Substring(0, 8)}";
        }
    }
} 