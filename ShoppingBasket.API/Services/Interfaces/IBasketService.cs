using ShoppingBasket.API.Domain.Entities;
using ShoppingBasket.API.Domain.Enums;

namespace ShoppingBasket.API.Services.Interfaces;

public interface IBasketService
{
    Task AddItemAsync(Product product, int quantity);

    Task AddItemsAsync(IEnumerable<(Product Product, int Quantity)> items);

    Task RemoveItemAsync(Guid productId);

    Task AddDiscountCodeAsync(DiscountCode code);

    Task SetShippingCountryAsync(Country country);

    Task<decimal> GetTotalWithVatAsync();

    Task<decimal> GetTotalWithoutVatAsync();
}
