using FluentValidation;
using OrderService.Application.DTOs;

namespace OrderService.Application.Validators
{
    public class AddressDtoValidator : AbstractValidator<AddressDto>
    {
        public AddressDtoValidator()
        {
            RuleFor(x => x.Street)
                .NotEmpty().WithMessage("Street is required.")
                .MaximumLength(100).WithMessage("Street cannot be longer than 100 characters.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(50).WithMessage("City cannot be longer than 50 characters.");

            RuleFor(x => x.State)
                .NotEmpty().WithMessage("State is required.")
                .MaximumLength(50).WithMessage("State cannot be longer than 50 characters.");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country is required.")
                .MaximumLength(50).WithMessage("Country cannot be longer than 50 characters.");

            RuleFor(x => x.ZipCode)
                .NotEmpty().WithMessage("Zip code is required.")
                .MaximumLength(10).WithMessage("Zip code cannot be longer than 10 characters.");
        }
    }
} 