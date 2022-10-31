using Dapper;
using Super3.Domain.Interfaces.Repositories;
using Super3.Domain.Interfaces.Repositories.DataConnector;
using Super3.Domain.Model;

namespace Super3.Infra.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly IDbConnector _dbConnector;

        public StockRepository(IDbConnector dbConnector)
        {
            _dbConnector = dbConnector;
        }
        public async Task<List<Stock>> GetAllAsync()
        {
            string sql = $@"SELECT s.ProductId
                            ,s.quantity
                            ,p.Id
                            ,p.productname
                        FROM stock s
                        JOIN product p ON s.productid = p.Id";

            var stocks = await _dbConnector.dbConnection.QueryAsync<Stock, Product, Stock>(
                sql: sql,
                map: (stock, product) =>
                {
                    stock.Product = product;
                    return stock;
                },

                splitOn: "Id",
                transaction: _dbConnector.dbTransaction);


            return stocks.ToList();
        }
        public async Task CreateAsync(Stock stock)
        {
            string sql = $@"INSERT INTO Stock 
                         (ProductId, Quantity) values (@ProductId, @Quantity)";

            await _dbConnector.dbConnection.ExecuteAsync(sql, new
            {
                ProductId = stock.Product.Id,
                Quantity = stock.Quantity,
            }, _dbConnector.dbTransaction);
        }

        public async Task<Stock> GetByIdAsync(string stockId)
        {
            string sql = $@"SELECT 
                             s.productId
                            ,s.quantity
                            ,p.Id
                            ,p.productname
                        FROM stock s
                        JOIN product p ON s.productid = p.Id
                        WHERE Id = @Id";


            var stock = await _dbConnector.dbConnection.QueryAsync<Stock, Product, Stock>(
                sql: sql,
                map: (stock, product) =>
                {
                    stock.Product = product;
                    return stock;
                },
                param: new { Id = stockId },
                splitOn: "Id",
                transaction: _dbConnector.dbTransaction);


            return stock.FirstOrDefault();
        }



        public async Task UpdateAsync(Stock stock)
        {
            string sql = $@"UPDATE Stock
                                SET [Quantity] = @Quantity
                                WHERE ProductId = @Id";

            await _dbConnector.dbConnection.ExecuteAsync(sql, new
            {
                Id = stock.Product.Id,
                Quantity = stock.Quantity,
            }, _dbConnector.dbTransaction); 
        }
    }
}
