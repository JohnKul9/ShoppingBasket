using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingBasket.API.Domain.Entities;
using ShoppingBasket.API.Domain.Enums;
using ShoppingBasket.API.DTOs;
using ShoppingBasket.API.Services.Interfaces;

namespace ShoppingBasket.API.Controllers;

[ApiController]
[Route("api/basket")]
public class BasketController : ControllerBase
{
    private readonly IBasketService _service;
    private readonly IMapper _mapper;
    private readonly IBasketValidationService _validation;

    public BasketController(
        IBasketService service,
        IMapper mapper,
        IBasketValidationService validation)
    {
        _service = service;
        _mapper = mapper;
        _validation = validation;
    }

    /// <summary>Add item to basket</summary>
    [HttpPost("item")]
    public async Task<IActionResult> AddItemAsync([FromBody] AddItemRequestDto dto)
    {
        ValidationResultDto validation = await _validation.ValidateAddItemAsync(dto);
        if (!validation.IsValid)
        {
            return BadRequest(validation.Errors);
        }

        Product product = _mapper.Map<Product>(dto);
        await _service.AddItemAsync(product, dto.Quantity);

        return Ok();
    }

    /// <summary>Add multiple items to the basket</summary>
    [HttpPost("items")]
    public async Task<IActionResult> AddItems([FromBody] List<AddItemRequestDto> items)
    {
        if (items == null || items.Count == 0)
        {
            return BadRequest("Items list cannot be empty.");
        }

        ValidationResultDto validation = await _validation.ValidateAddItemsAsync(items);
        if (!validation.IsValid)
        {
            return BadRequest(validation.Errors);
        }

        IEnumerable<(Product, int)> mappedItems = items.Select(dto =>
        (
            Product: _mapper.Map<Product>(dto),
            Quantity: dto.Quantity
        ));

        await _service.AddItemsAsync(mappedItems);

        return Ok();
    }

    /// <summary>Remove item from basket</summary>
    [HttpDelete("item/{productId}")]
    public async Task<IActionResult> RemoveItemAsync(Guid productId)
    {
        await _service.RemoveItemAsync(productId);

        return Ok();
    }

    /// <summary>Add discount code (not applied to discounted items)</summary>
    [HttpPost("discount")]
    public async Task<IActionResult> AddDiscountAsync([FromBody] DiscountCodeDto dto)
    {
        ValidationResultDto validation = await _validation.ValidateDiscountAsync(dto);
        if (!validation.IsValid)
        {
            return BadRequest(validation.Errors);
        }

        await _service.AddDiscountCodeAsync(new DiscountCode
        {
            Code = dto.Code,
            Percentage = dto.Percentage
        });

        return Ok();
    }

    /// <summary>Set shipping country</summary>
    [HttpPost("shipping/{country}")]
    public async Task<IActionResult> SetShippingAsync(string country)
    {
        ValidationResultDto validation = _validation.ValidateShippingCountry(country);
        if (!validation.IsValid)
        {
            return BadRequest(validation.Errors);
        }

        await _service.SetShippingCountryAsync(Enum.Parse<Country>(country, true));

        return Ok();
    }

    /// <summary>Total price with VAT</summary>
    [HttpGet("total/with-vat")]
    public async Task<IActionResult> GetTotalWithVatAsync()
    {
        return Ok(await _service.GetTotalWithVatAsync());
    }

    /// <summary>Total price without VAT</summary>
    [HttpGet("total/without-vat")]
    public async Task<IActionResult> GetTotalWithoutVatAsync()
    {
        return Ok(await _service.GetTotalWithoutVatAsync());
    }
}
