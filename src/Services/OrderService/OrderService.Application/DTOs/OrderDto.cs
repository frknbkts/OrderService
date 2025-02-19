using System;
using System.Collections.Generic;
using OrderService.Domain.Enums;

namespace OrderService.Application.DTOs
{
    public class OrderDto
    {
        public OrderDto()
        {
            OrderNumber = string.Empty;
            OrderItems = new List<OrderItemDto>();
            ShippingAddress = new AddressDto();
        }

        public Guid Id { get; set; }
        public string OrderNumber { get; set; }
        public Guid UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public AddressDto ShippingAddress { get; set; }
    }
} 