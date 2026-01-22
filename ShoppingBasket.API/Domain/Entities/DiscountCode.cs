namespace ShoppingBasket.API.Domain.Entities;

public class DiscountCode
{
    public string Code { get; set; } = string.Empty;

    public decimal Percentage { get; set; }

    public DiscountCode()
    {
        Code = string.Empty;
    }
}
