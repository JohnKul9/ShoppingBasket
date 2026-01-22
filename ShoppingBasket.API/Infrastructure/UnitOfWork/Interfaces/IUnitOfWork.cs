using ShoppingBasket.API.Infrastructure.Repositories.Interfaces;

namespace ShoppingBasket.API.Infrastructure.UnitOfWork.Interfaces;

public interface IUnitOfWork
{
    IBasketRepository BasketRepository { get; }

    Task CommitAsync();
}
