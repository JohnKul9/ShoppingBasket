namespace ShoppingBasket.API.Domain.Entities;

public class Product
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public bool IsDiscounted { get; set; }

    public Product()
    {
        Id = Guid.NewGuid();
        Name = string.Empty;
    }
}
