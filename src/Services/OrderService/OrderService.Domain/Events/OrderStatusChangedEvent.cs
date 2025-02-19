using System;
using OrderService.Domain.Enums;

namespace OrderService.Domain.Events
{
    public class OrderStatusChangedEvent
    {
        public Guid OrderId { get; }
        public string OrderNumber { get; }
        public Guid UserId { get; }
        public OrderStatus OldStatus { get; }
        public OrderStatus NewStatus { get; }
        public DateTime ChangedDate { get; }

        public OrderStatusChangedEvent(
            Guid orderId,
            string orderNumber,
            Guid userId,
            OrderStatus oldStatus,
            OrderStatus newStatus,
            DateTime changedDate)
        {
            OrderId = orderId;
            OrderNumber = orderNumber;
            UserId = userId;
            OldStatus = oldStatus;
            NewStatus = newStatus;
            ChangedDate = changedDate;
        }
    }
} 