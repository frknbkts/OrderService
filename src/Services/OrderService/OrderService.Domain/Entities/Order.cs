using System;
using System.Collections.Generic;
using OrderService.Domain.Exceptions;
using OrderService.Domain.ValueObjects;
using OrderService.Domain.Enums;

namespace OrderService.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; private set; }
        public string OrderNumber { get; private set; }
        public Guid UserId { get; private set; }
        public decimal TotalAmount { get; private set; }
        public OrderStatus Status { get; private set; }
        public DateTime OrderDate { get; private set; }
        public List<OrderItem> OrderItems { get; private set; }
        public Address ShippingAddress { get; private set; }

        private Order()
        {
            OrderItems = new List<OrderItem>();
        }

        public static Order Create(
            Guid userId,
            string orderNumber,
            Address shippingAddress)
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                OrderNumber = orderNumber,
                Status = OrderStatus.Created,
                OrderDate = DateTime.UtcNow,
                ShippingAddress = shippingAddress
            };

            return order;
        }

        public void AddOrderItem(Guid productId, string productName, decimal unitPrice, int quantity)
        {
            var orderItem = OrderItem.Create(productId, productName, unitPrice, quantity);
            OrderItems.Add(orderItem);
            CalculateTotalAmount();
        }

        public void UpdateStatus(OrderStatus newStatus)
        {
            if (!IsValidStatusTransition(Status, newStatus))
                throw new InvalidOrderStateException(Id, Status, newStatus);

            Status = newStatus;
        }

        private void CalculateTotalAmount()
        {
            TotalAmount = 0;
            foreach (var item in OrderItems)
            {
                TotalAmount += item.TotalPrice;
            }
        }

        private bool IsValidStatusTransition(OrderStatus currentStatus, OrderStatus newStatus)
        {
            return (currentStatus, newStatus) switch
            {
                (OrderStatus.Created, OrderStatus.Pending) => true,
                (OrderStatus.Pending, OrderStatus.Confirmed) => true,
                (OrderStatus.Pending, OrderStatus.Cancelled) => true,
                (OrderStatus.Confirmed, OrderStatus.Processing) => true,
                (OrderStatus.Processing, OrderStatus.Shipped) => true,
                (OrderStatus.Shipped, OrderStatus.Delivered) => true,
                (OrderStatus.Delivered, OrderStatus.Returned) => true,
                _ => false
            };
        }
    }
} 