using Microsoft.Extensions.Options;
using ShoppingBasket.API.Configuration;
using ShoppingBasket.API.Domain.Entities;
using ShoppingBasket.API.Domain.Enums;
using ShoppingBasket.API.Helpers;
using ShoppingBasket.API.Infrastructure.UnitOfWork.Interfaces;
using ShoppingBasket.API.Services.Interfaces;

namespace ShoppingBasket.API.Services;

public class BasketService : IBasketService
{
    private const decimal VatRate = 0.20m;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ShippingSettings _shippingSettings;

    public BasketService(
        IUnitOfWork unitOfWork,
        IOptions<ShippingSettings> shippingOptions)
    {
        _unitOfWork = unitOfWork;
        _shippingSettings = shippingOptions.Value;
    }

    public async Task AddItemAsync(Product product, int quantity)
    {
        Basket basket = await _unitOfWork.BasketRepository.GetAsync();

        BasketItem existing = basket.Items.FirstOrDefault(i => i.Product.Id == product.Id);
        if (existing != null)
        {
            existing.Quantity += quantity;
        }
        else
        {
            basket.Items.Add(new BasketItem
            {
                Product = product,
                Quantity = quantity
            });
        }

        await _unitOfWork.CommitAsync();
    }

    public async Task AddItemsAsync(IEnumerable<(Product Product, int Quantity)> items)
    {
        List<BasketItem> basketItems = items
            .Select(i => new BasketItem
            {
                Product = i.Product,
                Quantity = i.Quantity
            })
            .ToList();

        await _unitOfWork.BasketRepository.AddRangeAsync(basketItems);
        await _unitOfWork.CommitAsync();
    }

    public async Task RemoveItemAsync(Guid productId)
    {
        Basket basket = await _unitOfWork.BasketRepository.GetAsync();
        basket.Items.RemoveAll(x => x.Product.Id == productId);

        await _unitOfWork.CommitAsync();
    }

    public async Task AddDiscountCodeAsync(DiscountCode code)
    {
        Basket basket = await _unitOfWork.BasketRepository.GetAsync();
        basket.DiscountCode = code;

        await _unitOfWork.CommitAsync();
    }

    public async Task SetShippingCountryAsync(Country country)
    {
        Basket basket = await _unitOfWork.BasketRepository.GetAsync();
        basket.ShippingCountry = country;

        await _unitOfWork.CommitAsync();
    }

    public async Task<decimal> GetTotalWithoutVatAsync()
    {
        Basket basket = await _unitOfWork.BasketRepository.GetAsync();

        decimal itemsTotal = basket.Items.Sum(x => x.Product.Price * x.Quantity);
        decimal discount = GetDiscountAmount(basket);
        decimal shipping = GetShippingCost(basket.ShippingCountry);

        decimal finalTotal = itemsTotal - discount + shipping;

        return MoneyRoundingHelper.RoundUp(finalTotal, 2);
    }

    public async Task<decimal> GetTotalWithVatAsync()
    {
        Basket basket = await _unitOfWork.BasketRepository.GetAsync();

        decimal itemsTotal = basket.Items.Sum(x => x.Product.Price * x.Quantity);
        decimal discount = GetDiscountAmount(basket);
        decimal shipping = GetShippingCost(basket.ShippingCountry);

        decimal finalTotal = ((itemsTotal - discount) * (1m + VatRate)) + shipping;

        return MoneyRoundingHelper.RoundUp(finalTotal, 2);
    }

    private static decimal GetDiscountAmount(Basket basket)
    {
        if (basket.DiscountCode is null)
        {
            return 0;
        }

        decimal eligibleTotal = basket.Items
            .Where(x => !x.Product.IsDiscounted)
            .Sum(x => x.Product.Price * x.Quantity);

        return eligibleTotal * (basket.DiscountCode.Percentage / 100m);
    }

    private decimal GetShippingCost(Country country)
    {
        return country == Country.UK
            ? _shippingSettings.UK
            : _shippingSettings.Other;
    }
}
