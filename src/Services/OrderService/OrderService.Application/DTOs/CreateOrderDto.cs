using System;
using System.Collections.Generic;

namespace OrderService.Application.DTOs
{
    public class CreateOrderDto
    {
        public CreateOrderDto()
        {
            OrderItems = new List<OrderItemDto>();
            ShippingAddress = new AddressDto();
        }

        public Guid UserId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public AddressDto ShippingAddress { get; set; }
    }

    public class OrderItemDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }

    public class AddressDto
    {
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
    }
} 