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
,c.Id
,oi.orderId as Id
,oi.productprice
,oi.totalamount
,p.Id
,p.productName
FROM [order] o
JOIN Customer c ON o.customerId = c.Id
JOIN OrderItem oi ON oi.orderId = o.Id
JOIN Product p ON oi.ProductId = p.Id";

            var orders = await _dbConnector.dbConnection.QueryAsync<Order, Customer, OrderItem, Product ,Order>(
                sql: sql,
                map: (order, customer, orderItem, product) =>
                {
                    order.Item = orderItem;
                    order.Product = product;
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
                            ,c.Id
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
            string sql = $@"SELECT oi.orderId as Id
                               ,oi.productprice
                               ,oi.totalamount
                               ,o.Id
                               ,o.totalprice
                               ,p.Id as Id
                               ,p.productname
                           FROM OrderItem oi
                           JOIN product p ON oi.productId = p.Id
                           JOIN [order] o ON oi.OrderId = o.Id
                           WHERE oi.OrderId = @OrderId";
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
                               ([Id]
                               ,[CustomerId]
                               ,[OrderNumber]
                               ,[OrderDate]
                               ,[TotalPrice])
                            VALUES
                               (@Id
                               ,@CustomerId
                               ,@OrderNumber
                               ,@OrderDate
                               ,@TotalPrice);

";

            await _dbConnector.dbConnection.ExecuteAsync(sql, new
            {
                //ProductId = order.Product.Id,
                Id = order.Id,
                CustomerId = order.Customer.Id,
                OrderNumber = order.OrderNumber,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice
            }, _dbConnector.dbTransaction);
            
            if (order.Items.Any())
            {
                foreach (var item in order.Items)
                {
                    item.Order = order;
                    await CreateItemAsync(item);
                }
            }
        }

        public async Task CreateItemAsync(OrderItem item)
        {
            string sql = $@"INSERT INTO OrderItem
                               ([OrderId]
                               ,[ProductId]
                               ,[ProductPrice]
                               ,[TotalAmount])
                            VALUES
                               (@OrderId
                               ,@ProductId
                               ,@ProductPrice
                               ,@TotalAmount);

UPDATE Stock
SET quantity = quantity - @TotalAmount
WHERE ProductId = @ProductId;


";
            await _dbConnector.dbConnection.ExecuteAsync(sql, new
            {
                OrderId = item.Order.Id,
                ProductId = item.Product.Id,
                ProductPrice = item.ProductPrice,
                TotalAmount = item.TotalAmount
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
