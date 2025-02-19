using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OrderService.Application.DTOs;
using OrderService.Domain.Repositories;

namespace OrderService.Application.Queries.GetUserOrders
{
    public class GetUserOrdersQueryHandler : IRequestHandler<GetUserOrdersQuery, IEnumerable<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetUserOrdersQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<OrderDto>> Handle(GetUserOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetByUserIdAsync(request.UserId);

            return orders.Select(order => new OrderDto
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                UserId = order.UserId,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                OrderDate = order.OrderDate,
                OrderItems = order.OrderItems.Select(item => new OrderItemDto
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity
                }).ToList(),
                ShippingAddress = new AddressDto
                {
                    Street = order.ShippingAddress.Street,
                    City = order.ShippingAddress.City,
                    State = order.ShippingAddress.State,
                    Country = order.ShippingAddress.Country,
                    ZipCode = order.ShippingAddress.ZipCode
                }
            });
        }
    }
} 