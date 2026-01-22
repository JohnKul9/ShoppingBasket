using FluentValidation;
using ShoppingBasket.API.DTOs;

namespace ShoppingBasket.API.Validators;

public class DiscountCodeDtoValidator : AbstractValidator<DiscountCodeDto>
{
    public DiscountCodeDtoValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage("Discount code is required");

        RuleFor(x => x.Percentage)
            .InclusiveBetween(0.1m, 100m)
            .WithMessage("Discount percentage must be between 0.1 and 100");
    }
}
