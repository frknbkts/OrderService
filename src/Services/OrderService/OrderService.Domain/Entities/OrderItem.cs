using System;

namespace OrderService.Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }
        public decimal TotalPrice { get; private set; }

        private OrderItem() { }

        public static OrderItem Create(
            Guid productId,
            string productName,
            decimal unitPrice,
            int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

            if (unitPrice <= 0)
                throw new ArgumentException("Unit price must be greater than zero.", nameof(unitPrice));

            var orderItem = new OrderItem
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                ProductName = productName,
                UnitPrice = unitPrice,
                Quantity = quantity
            };

            orderItem.CalculateTotalPrice();
            return orderItem;
        }

        private void CalculateTotalPrice()
        {
            TotalPrice = UnitPrice * Quantity;
        }
    }
} 