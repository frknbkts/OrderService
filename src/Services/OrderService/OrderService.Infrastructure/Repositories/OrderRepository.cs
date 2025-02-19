using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;
using OrderService.Domain.Exceptions;
using OrderService.Domain.Repositories;
using OrderService.Infrastructure.Persistence;
using OrderService.Domain.Enums;

namespace OrderService.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _context;

        public OrderRepository(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                throw new OrderNotFoundException(id);

            return order;
        }

        public async Task<Order> GetByOrderNumberAsync(string orderNumber)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);

            if (order == null)
                throw new OrderNotFoundException(orderNumber);

            return order;
        }

        public async Task<IEnumerable<Order>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetByStatusAsync(OrderStatus status)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.Status == status)
                .ToListAsync();
        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            var existingOrder = await GetByIdAsync(order.Id);
            _context.Entry(existingOrder).CurrentValues.SetValues(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var order = await GetByIdAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }
} 