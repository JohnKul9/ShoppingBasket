using FluentValidation;
using ShoppingBasket.API.Configuration;
using ShoppingBasket.API.Infrastructure.Repositories;
using ShoppingBasket.API.Infrastructure.Repositories.Interfaces;
using ShoppingBasket.API.Infrastructure.UnitOfWork;
using ShoppingBasket.API.Infrastructure.UnitOfWork.Interfaces;
using ShoppingBasket.API.Mapping;
using ShoppingBasket.API.Services;
using ShoppingBasket.API.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(BasketProfile));
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.Configure<ShippingSettings>(
    builder.Configuration.GetSection("Shipping"));

builder.Services.AddSingleton<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.AddScoped<IBasketValidationService, BasketValidationService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
