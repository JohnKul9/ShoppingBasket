using ShoppingBasket.API.Infrastructure.Repositories.Interfaces;
using ShoppingBasket.API.Infrastructure.UnitOfWork.Interfaces;

namespace ShoppingBasket.API.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    public IBasketRepository BasketRepository { get; }

    public UnitOfWork(IBasketRepository basketRepository)
    {
        BasketRepository = basketRepository;
    }

    public Task CommitAsync()
    {
        return Task.CompletedTask;
    }
}
