namespace ShoppingBasket.API.DTOs;

public class AddItemRequestDto
{
    public string Name { get; set; }

    public decimal Price { get; set; }

    public bool IsDiscounted { get; set; }

    public int Quantity { get; set; }

    public AddItemRequestDto()
    {
        Name = string.Empty;
    }
}
