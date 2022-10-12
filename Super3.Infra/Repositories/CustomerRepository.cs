using Dapper;
using Super3.Domain.Interfaces.Repositories;
using Super3.Domain.Interfaces.Repositories.DataConnector;
using Super3.Domain.Model;

namespace Super3.Infra.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDbConnector _dbConnector;

        public CustomerRepository(IDbConnector dbConnector)
        {
            _dbConnector = dbConnector;
        }
        
        public async Task<List<Customer>> GetAllAsync()
        {
            string sql = $@"SELECT [Id]
                          ,[FirstName]
                          ,[LastName]
                          ,[Document]
                          ,[Street]
                          ,[AddressNumber]
                          ,[Neighborhood]
                          ,[City]
                          ,[Province]
                          ,[CEP]
                      FROM [dbo].[Customer]";

            var customers = await _dbConnector.dbConnection.QueryAsync<Customer>(sql, _dbConnector.dbTransaction);

            return customers.ToList();
        }

        public async Task<Customer> GetByIdAsync(int customerId)
        {
            string sql = $@"SELECT [Id]
                          ,[FirstName]
                          ,[LastName]
                          ,[Document]
                          ,[Street]
                          ,[AddressNumber]
                          ,[Neighborhood]
                          ,[City]
                          ,[Province]
                          ,[CEP]
                      FROM [dbo].[Customer]
                      WHERE Id = @Id";


            var customer = await _dbConnector.dbConnection.QueryAsync<Customer>(sql, new { Id = customerId }, _dbConnector.dbTransaction);

            return customer.FirstOrDefault();
        }

        public async Task CreateAsync(Customer customer)
        {
            string sql = $@"INSERT INTO [dbo].[Customer]
                           ([FirstName]
                           ,[LastName]
                           ,[Document]
                           ,[Street]
                           ,[AddressNumber]
                           ,[Neighborhood]
                           ,[City]
                           ,[Province]
                           ,[CEP])
                     VALUES
                           (@FirstName
                           ,@LastName
                           ,@Document
                           ,@Street
                           ,@AddressNumber
                           ,@Neighborhood
                           ,@City
                           ,@Province
                           ,@CEP)";

            await _dbConnector.dbConnection.ExecuteAsync(sql, new
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Document = customer.Document.Replace("-", "").Replace(".", ""),
                Street = customer.Street,
                AddressNumber = customer.AddressNumber,
                Neighborhood = customer.Neighborhood,
                City = customer.City,
                Province = customer.Province,
                CEP = customer.CEP.Replace("-", "").Replace(".", "")
            }, _dbConnector.dbTransaction);
        }

        public async Task UpdateAsync(Customer customer)
        {
            string sql = @"UPDATE Customer
                            SET 
                                FirstName = @FirstName
                               ,LastName = @LastName
                               ,Document = @Document
                               ,Street = @Street
                               ,AddressNumber = @AddressNumber
                               ,Neighborhood = @Neighborhood
                               ,City = @City
                               ,Province = @Province
                               ,CEP = @CEP
                            WHERE Id = @Id";

            await _dbConnector.dbConnection.ExecuteAsync(sql, new
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Document = customer.Document,
                Street = customer.Street,
                AddressNumber = customer.AddressNumber,
                Neighborhood = customer.Neighborhood,
                City = customer.City,
                Province = customer.Province,
                CEP = customer.CEP.Replace("-", "")
            }, _dbConnector.dbTransaction);
        }

        public async Task<bool> ExistsByIdAsync(int customerId)
        {
            string sql = $@"SELECT 1 FROM Customer WHERE Id = @Id ";


            var customer = await _dbConnector.dbConnection.QueryAsync<bool>(sql, new { Id = customerId }, _dbConnector.dbTransaction);

            return customer.FirstOrDefault();
        }

        public async Task<bool> CpfExists(string cpf)
        {
            string sql = $@"SELECT 1 FROM Customer WHERE Document = @Document ";


            var customerCPF = await _dbConnector.dbConnection.QueryAsync<bool>(sql, new { Document = cpf }, _dbConnector.dbTransaction);

            return customerCPF.FirstOrDefault();
        }

    }
}
