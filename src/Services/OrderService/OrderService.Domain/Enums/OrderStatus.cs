namespace OrderService.Domain.Enums
{
    public enum OrderStatus
    {
        Created = 0,
        Pending = 1,
        Confirmed = 2,
        Processing = 3,
        Shipped = 4,
        Delivered = 5,
        Cancelled = 6,
        Returned = 7
    }
} 