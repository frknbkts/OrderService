using System;

namespace OrderService.Domain.Exceptions
{
    public class OrderNotFoundException : OrderDomainException
    {
        public OrderNotFoundException(Guid orderId)
            : base($"Order with ID {orderId} was not found.")
        {
            OrderId = orderId;
        }

        public OrderNotFoundException(string orderNumber)
            : base($"Order with number {orderNumber} was not found.")
        {
            OrderNumber = orderNumber;
        }

        public Guid? OrderId { get; }
        public string OrderNumber { get; }
    }
} 