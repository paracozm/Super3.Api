using Super3.Domain.Interfaces.Repositories;
using Super3.Domain.Interfaces.Repositories.DataConnector;

namespace Super3.Infra.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private ICustomerRepository _customerRepository;
        private IOrderRepository _orderRepository;
        private IProductRepository _productRepository;
        private IStockRepository _stockRepository;

        public UnitOfWork(IDbConnector dbConnector)
        {
            this.dbConnector = dbConnector;
        }

        public ICustomerRepository CustomerRepository => _customerRepository ?? (_customerRepository = new CustomerRepository(dbConnector));

        public IOrderRepository OrderRepository => _orderRepository ?? (_orderRepository = new OrderRepository(dbConnector));

        public IProductRepository ProductRepository => _productRepository ?? (_productRepository = new ProductRepository(dbConnector));

        public IStockRepository StockRepository => _stockRepository ?? (_stockRepository = new StockRepository(dbConnector));

        public IDbConnector dbConnector { get; }

        public void BeginTransaction()
        {
            if(dbConnector.dbConnection.State == System.Data.ConnectionState.Open)
                dbConnector.dbConnection.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
        }

        public void CommitTransaction()
        {
            dbConnector.dbTransaction.Commit();
        }

        public void RollbackTransaction()
        {
            dbConnector.dbTransaction.Rollback();
        }
    }
}
