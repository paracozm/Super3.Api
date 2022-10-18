using Super3.Domain.Interfaces.Repositories.DataConnector;

namespace Super3.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        ICustomerRepository CustomerRepository { get; }
        IOrderRepository OrderRepository { get; }
        IProductRepository ProductRepository { get; }
        IStockRepository StockRepository { get; }
        IDbConnector dbConnector { get; }

        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();

    }
}
