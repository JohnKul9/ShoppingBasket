using FluentValidation;
using FluentValidation.Results;
using ShoppingBasket.API.Domain.Enums;
using ShoppingBasket.API.DTOs;
using ShoppingBasket.API.Services.Interfaces;

namespace ShoppingBasket.API.Services;

public class BasketValidationService : IBasketValidationService
{
    private readonly IValidator<AddItemRequestDto> _addItemValidator;
    private readonly IValidator<DiscountCodeDto> _discountValidator;

    public BasketValidationService(
        IValidator<AddItemRequestDto> addItemValidator,
        IValidator<DiscountCodeDto> discountValidator)
    {
        _addItemValidator = addItemValidator;
        _discountValidator = discountValidator;
    }

    public async Task<ValidationResultDto> ValidateAddItemAsync(
        AddItemRequestDto dto)
    {
        ValidationResult result = await _addItemValidator.ValidateAsync(dto);

        return result.IsValid
            ? ValidationResultDto.Success()
            : ValidationResultDto.Failure(MapErrors(result.Errors));
    }

    public async Task<ValidationResultDto> ValidateAddItemsAsync(
        IEnumerable<AddItemRequestDto> items)
    {
        List<object> errors = [];
        int index = 0;

        foreach (AddItemRequestDto item in items)
        {
            ValidationResult result = await _addItemValidator.ValidateAsync(item);

            if (!result.IsValid)
            {
                errors.Add(new
                {
                    Index = index,
                    Item = item,
                    Errors = MapErrors(result.Errors)
                });
            }

            index++;
        }

        return errors.Any()
            ? ValidationResultDto.Failure(errors)
            : ValidationResultDto.Success();
    }

    public async Task<ValidationResultDto> ValidateDiscountAsync(DiscountCodeDto dto)
    {
        ValidationResult result = await _discountValidator.ValidateAsync(dto);

        return result.IsValid
            ? ValidationResultDto.Success()
            : ValidationResultDto.Failure(MapErrors(result.Errors));
    }

    public ValidationResultDto ValidateShippingCountry(string country)
    {
        if (!Enum.TryParse<Country>(country, true, out _))
        {
            return ValidationResultDto.Failure(
                [
                    new
                    {
                        Field = "country",
                        Message = "Invalid country. Allowed values: UK, Other"
                    }
                ]);
        }

        return ValidationResultDto.Success();
    }

    private static List<object> MapErrors(IEnumerable<ValidationFailure> failures)
    {
        return failures
            .Select(e => new
            {
                Field = e.PropertyName,
                Message = e.ErrorMessage
            })
            .Cast<object>()
            .ToList();
    }
}
