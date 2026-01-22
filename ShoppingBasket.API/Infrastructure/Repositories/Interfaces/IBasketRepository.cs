using ShoppingBasket.API.Domain.Entities;

namespace ShoppingBasket.API.Infrastructure.Repositories.Interfaces;

public interface IBasketRepository
{
    Task<Basket> GetAsync();

    Task AddRangeAsync(IEnumerable<BasketItem> items);

    Task SaveAsync(Basket basket);
}
