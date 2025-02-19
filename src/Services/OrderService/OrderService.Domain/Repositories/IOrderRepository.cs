using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderService.Domain.Entities;
using OrderService.Domain.Enums;

namespace OrderService.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetByIdAsync(Guid id);
        Task<Order> GetByOrderNumberAsync(string orderNumber);
        Task<IEnumerable<Order>> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<Order>> GetByStatusAsync(OrderStatus status);
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(Guid id);
    }
} 