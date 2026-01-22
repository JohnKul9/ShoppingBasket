namespace ShoppingBasket.API.DTOs;

public class BasketItemDto
{
    public Guid ProductId { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public bool IsDiscounted { get; set; }

    public int Quantity { get; set; }

    public BasketItemDto()
    {
        Name = string.Empty;
    }
}
