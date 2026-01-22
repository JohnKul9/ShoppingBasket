using FluentValidation;
using ShoppingBasket.API.DTOs;

namespace ShoppingBasket.API.Validators;

public class AddItemRequestDtoValidator : AbstractValidator<AddItemRequestDto>
{
    public AddItemRequestDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Product name is required");

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than zero");

        RuleFor(x => x.Price)
            .Must(HaveMaxTwoDecimalPlaces)
            .WithMessage("Price must have a maximum of 2 decimal places");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be at least 1");
    }

    private static bool HaveMaxTwoDecimalPlaces(decimal price)
    {
        return decimal.Round(price, 2) == price;
    }
}
