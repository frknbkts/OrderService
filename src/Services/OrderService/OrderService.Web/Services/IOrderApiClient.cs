using OrderService.Application.DTOs;

namespace OrderService.Web.Services
{
    public interface IOrderApiClient
    {
        Task<IEnumerable<OrderDto>> GetUserOrdersAsync(Guid userId);
        Task<OrderDto?> GetOrderByIdAsync(Guid orderId);
        Task<Guid> CreateOrderAsync(CreateOrderDto createOrderDto);
    }
} 