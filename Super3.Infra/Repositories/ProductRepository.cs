using Dapper;
using Super3.Domain.Interfaces.Repositories;
using Super3.Domain.Interfaces.Repositories.DataConnector;
using Super3.Domain.Model;

namespace Super3.Infra.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnector _dbConnector;

        public ProductRepository(IDbConnector dbConnector)
        {
            _dbConnector = dbConnector;
        }
        public async Task<List<Product>> GetAllAsync()
        {
            string sql = $@"SELECT [Id]
                                 ,[ProductName]
                             FROM [dbo].[Product]";

            var products = await _dbConnector.dbConnection.QueryAsync<Product>(sql, _dbConnector.dbTransaction);

            return products.ToList();
        }
        public async Task<Product> GetByIdAsync(int productId)
        {
            string sql = $@"SELECT [Id]
                                 ,[ProductName]
                             FROM [dbo].[Product]
                             WHERE Id = @Id";


            var product = await _dbConnector.dbConnection.QueryAsync<Product>(sql, new { Id = productId }, _dbConnector.dbTransaction);

            return product.FirstOrDefault();
        }
        public async Task CreateAsync(Product product)
        {
            string sql = $@"INSERT INTO Product 
                         (ProductName) values (@ProductName)";

            await _dbConnector.dbConnection.ExecuteAsync(sql, new
            {
                ProductName = product.ProductName
            }, _dbConnector.dbTransaction);
        }
        public async Task UpdateAsync(Product product)
        {
            string sql = $@"UPDATE Product
                                SET [ProductName] = @ProductName
                                WHERE Id = @Id";

            await _dbConnector.dbConnection.ExecuteAsync(sql, new
            {
                ProductName = product.ProductName
            }, _dbConnector.dbTransaction);

        }
        public async Task<bool> ExistsByIdAsync(int productId)
        {
            string sql = $@"SELECT 1 FROM Product WHERE Id = @Id ";


            var product = await _dbConnector.dbConnection.QueryAsync<bool>(sql, new { Id = productId }, _dbConnector.dbTransaction);

            return product.FirstOrDefault();
        }

    }
}
