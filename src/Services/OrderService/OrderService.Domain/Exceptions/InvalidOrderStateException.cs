using System;
using OrderService.Domain.Enums;

namespace OrderService.Domain.Exceptions
{
    public class InvalidOrderStateException : OrderDomainException
    {
        public InvalidOrderStateException(Guid orderId, OrderStatus currentStatus, OrderStatus newStatus)
            : base($"Cannot change order {orderId} status from {currentStatus} to {newStatus}.")
        {
            OrderId = orderId;
            CurrentStatus = currentStatus;
            NewStatus = newStatus;
        }

        public Guid OrderId { get; }
        public OrderStatus CurrentStatus { get; }
        public OrderStatus NewStatus { get; }
    }
} 