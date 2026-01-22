namespace ShoppingBasket.API.DTOs;

public class DiscountCodeDto
{
    public string Code { get; set; }

    public decimal Percentage { get; set; }

    public DiscountCodeDto()
    {
        Code = string.Empty;
    }
}
