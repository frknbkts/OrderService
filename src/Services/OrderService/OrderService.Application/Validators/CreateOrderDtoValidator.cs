using FluentValidation;
using OrderService.Application.DTOs;

namespace OrderService.Application.Validators
{
    public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderDtoValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(x => x.OrderItems)
                .NotEmpty().WithMessage("Order must contain at least one item.")
                .Must(items => items.Count <= 10).WithMessage("Order cannot contain more than 10 items.");

            RuleForEach(x => x.OrderItems)
                .SetValidator(new OrderItemDtoValidator());

            RuleFor(x => x.ShippingAddress)
                .NotNull().WithMessage("Shipping address is required.")
                .SetValidator(new AddressDtoValidator());
        }
    }
} 