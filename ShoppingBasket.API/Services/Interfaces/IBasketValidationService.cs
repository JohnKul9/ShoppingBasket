using ShoppingBasket.API.DTOs;

namespace ShoppingBasket.API.Services.Interfaces;

public interface IBasketValidationService
{
    Task<ValidationResultDto> ValidateAddItemAsync(AddItemRequestDto dto);

    Task<ValidationResultDto> ValidateAddItemsAsync(IEnumerable<AddItemRequestDto> items);

    Task<ValidationResultDto> ValidateDiscountAsync(DiscountCodeDto dto);

    ValidationResultDto ValidateShippingCountry(string country);
}
