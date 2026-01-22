namespace ShoppingBasket.API.Domain.Entities;

public class BasketItem
{
    public Product Product { get; set; }

    public int Quantity { get; set; }

    public BasketItem()
    {
        Product = default!;
    }
}
