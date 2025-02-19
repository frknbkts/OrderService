using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OrderService.Application.DTOs;
using OrderService.Domain.Repositories;

namespace OrderService.Application.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto?>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _orderRepository.GetByIdAsync(request.OrderId);
                
                if (order == null)
                    return null;

                return new OrderDto
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
                };
            }
            catch
            {
                return null;
            }
        }
    }
} 