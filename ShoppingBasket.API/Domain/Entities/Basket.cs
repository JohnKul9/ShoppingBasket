using ShoppingBasket.API.Domain.Enums;

namespace ShoppingBasket.API.Domain.Entities;

public class Basket
{
    public List<BasketItem> Items { get; set; }

    public DiscountCode? DiscountCode { get; set; }

    public Country ShippingCountry { get; set; }

    public Basket()
    {
        Items = [];
        ShippingCountry = Country.UK;
    }
}
