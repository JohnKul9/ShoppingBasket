namespace ShoppingBasket.API.DTOs;

public class ValidationResultDto
{
    public bool IsValid { get; set; }

    public List<object> Errors { get; set; }

    public ValidationResultDto(bool isValid, List<object> errors)
    {
        IsValid = isValid;
        Errors = errors;
    }

    public static ValidationResultDto Success()
        => new(true, []);

    public static ValidationResultDto Failure(List<object> errors)
        => new(false, errors);
}
