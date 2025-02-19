using System;
using System.Collections.Generic;

namespace OrderService.Domain.Events
{
    public class OrderCreatedEvent
    {
        public Guid OrderId { get; }
        public string OrderNumber { get; }
        public Guid UserId { get; }
        public decimal TotalAmount { get; }
        public List<OrderItemEvent> OrderItems { get; }
        public AddressEvent ShippingAddress { get; }
        public DateTime OrderDate { get; }

        public OrderCreatedEvent(
            Guid orderId,
            string orderNumber,
            Guid userId,
            decimal totalAmount,
            List<OrderItemEvent> orderItems,
            AddressEvent shippingAddress,
            DateTime orderDate)
        {
            OrderId = orderId;
            OrderNumber = orderNumber;
            UserId = userId;
            TotalAmount = totalAmount;
            OrderItems = orderItems;
            ShippingAddress = shippingAddress;
            OrderDate = orderDate;
        }
    }

    public class OrderItemEvent
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class AddressEvent
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }
} 