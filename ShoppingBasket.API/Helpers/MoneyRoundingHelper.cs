namespace ShoppingBasket.API.Helpers;

public static class MoneyRoundingHelper
{
    public static decimal RoundUp(decimal value, int decimalPlaces = 2)
    {
        decimal factor = (decimal)Math.Pow(10, decimalPlaces);

        return Math.Ceiling(value * factor) / factor;
    }
}
