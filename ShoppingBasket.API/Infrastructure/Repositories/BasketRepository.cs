using ShoppingBasket.API.Domain.Entities;
using ShoppingBasket.API.Infrastructure.Repositories.Interfaces;

namespace ShoppingBasket.API.Infrastructure.Repositories;

public class BasketRepository : IBasketRepository
{
    private static Basket _basket = new();
    private static readonly SemaphoreSlim _lock = new(1, 1);

    public async Task<Basket> GetAsync()
    {
        await _lock.WaitAsync();

        try
        {
            return _basket;
        }
        finally
        {
            _lock.Release();
        }
    }

    public async Task AddRangeAsync(IEnumerable<BasketItem> items)
    {
        await _lock.WaitAsync();

        try
        {
            foreach (BasketItem item in items)
            {
                BasketItem existing = _basket.Items
                    .FirstOrDefault(x => x.Product.Id == item.Product.Id);

                if (existing != null)
                {
                    existing.Quantity += item.Quantity;
                }
                else
                {
                    _basket.Items.Add(item);
                }
            }
        }
        finally
        {
            _lock.Release();
        }
    }

    public async Task SaveAsync(Basket basket)
    {
        await _lock.WaitAsync();

        try
        {
            _basket = basket;
        }
        finally
        {
            _lock.Release();
        }
    }
}
