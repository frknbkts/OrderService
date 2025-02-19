using FluentValidation;
using OrderService.Application.DTOs;

namespace OrderService.Application.Validators
{
    public class OrderItemDtoValidator : AbstractValidator<OrderItemDto>
    {
        public OrderItemDtoValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("Product ID is required.");

            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name cannot be longer than 100 characters.");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0).WithMessage("Unit price must be greater than zero.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.")
                .LessThanOrEqualTo(100).WithMessage("Quantity cannot exceed 100 items.");
        }
    }
} 