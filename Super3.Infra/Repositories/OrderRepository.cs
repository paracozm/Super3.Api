using Dapper;
using Super3.Domain.Interfaces.Repositories;
using Super3.Domain.Interfaces.Repositories.DataConnector;
using Super3.Domain.Model;

namespace Super3.Infra.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IDbConnector _dbConnector;

        public OrderRepository(IDbConnector dbConnector)
        {
            _dbConnector = dbConnector;
        }
        public async Task<List<Order>> GetAllAsync()
        {
            string sql = $@"SELECT o.Id
                            ,o.ordernumber
                            ,o.orderdate
                            ,o.totalprice
                            ,c.Id as customerId
                            ,c.firstname
                            ,c.lastname
                            ,c.document
                            ,c.street
                            ,c.addressnumber
                            ,c.Neighborhood
                            ,c.city
                            ,c.province
                            ,c.cep
                        FROM [order] o
                        JOIN Customer c ON o.customerId = c.Id";

            var orders = await _dbConnector.dbConnection.QueryAsync<Order, Customer, Order>(
                sql: sql,
                map: (order, customer) =>
                {
                    order.Customer = customer;
                    return order;
                },

                splitOn: "Id",
                transaction: _dbConnector.dbTransaction);


            return orders.ToList();
        }

        public async Task<Order> GetByIdAsync(string orderId)
        {
            string sql = $@"SELECT o.Id
                            ,o.ordernumber
                            ,o.orderdate
                            ,o.totalprice
                            ,c.Id as customerId
                            ,c.firstname
                            ,c.lastname
                            ,c.document
                            ,c.street
                            ,c.addressnumber
                            ,c.Neighborhood
                            ,c.city
                            ,c.province
                            ,c.cep
                        FROM [order] o
                        JOIN Customer c ON o.customerId = c.Id";

            var order = await _dbConnector.dbConnection.QueryAsync<Order, Customer, Order>(
                sql: sql,
                map: (order, customer) =>
                {
                    order.Customer = customer;
                    return order;
                },
                param: new { Id = orderId },
                splitOn: "Id",
                transaction: _dbConnector.dbTransaction);


            return order.FirstOrDefault();
        }

        public async Task<List<OrderItem>> ListItemByOrderIdAsync(string orderId)
        {
            string sql = $@"SELECT oi.Id
                               ,oi.productprice
                               ,o.Id as orderId
                               ,o.ordernumber
                               ,o.orderdate
                               ,o.totalprice
                               ,p.Id as productId
                               ,p.productname
                           FROM OrderItem oi
                           JOIN [order] o ON oi.orderid = o.Id
                           JOIN Product p ON oi.productId = p.Id";
            var items = await _dbConnector.dbConnection.QueryAsync<OrderItem, Order, Product, OrderItem>(

                sql: sql,
                map: (item, order, product) =>
                {
                    item.Order = order;
                    item.Product = product;
                    return item;
                },
                param: new { OrderId = orderId },
                splitOn: "Id",
                transaction: _dbConnector.dbTransaction);
            return items.ToList();



        }
        
        
        
        
        public async Task CreateAsync(Order order)
        {
            string sql = $@"INSERT INTO [order]
                               ([CustomerId]
                               ,[OrderNumber]
                               ,[OrderDate]
                               ,[TotalPrice])
                            VALUES
                               (@CustomerId
                               ,@OrderNumber
                               ,@OrderDate
                               ,@TotalPrice)";

            await _dbConnector.dbConnection.ExecuteAsync(sql, new
            {
                CustomerId = order.Customer.Id,
                OrderNumber = order.OrderNumber,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice
            }, _dbConnector.dbTransaction);

            if (order.Items.Any())
            {
                foreach (var item in order.Items)
                {
                    await CreateItemAsync(item);
                }
            }

        }

        public async Task CreateItemAsync(OrderItem item)
        {
            string sql = $@"INSERT INTO OrderItem
                               ([OrderId]
                               ,[ProductId]
                               ,[ProductPrice])
                            VALUES
                               (@OrderId
                               ,@ProductId
                               ,@ProductPrice)";
            await _dbConnector.dbConnection.ExecuteAsync(sql, new
            {
                OrderId = item.Order.Id,
                ProductId = item.Product.Id,
                ProductPrice = item.ProductPrice,
            }, _dbConnector.dbTransaction);
        }





        public async Task<bool> ExistsByIdAsync(string orderId)
        {
            string sql = $@"SELECT 1 FROM [Order] WHERE Id = @Id ";

            var order = await _dbConnector.dbConnection.QueryAsync<bool>(sql, new { Id = orderId }, _dbConnector.dbTransaction);

            return order.FirstOrDefault();
        }

    }
}
