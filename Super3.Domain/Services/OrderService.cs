using Super3.Domain.Interfaces.Repositories;
using Super3.Domain.Interfaces.Services;
using Super3.Domain.Model;
using Super3.Domain.Validations;
using Super3.Domain.Validations.Base;

namespace Super3.Domain.Services
{
    public class OrderService : IOrderService
    {
        
        private readonly IUnitOfWork _unitOfWork;
        public OrderService (IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<List<Order>>> GetAllAsync()
        {
            var response = new Response<List<Order>>();
            var data = await _unitOfWork.OrderRepository.GetAllAsync();

            

            response.Data = data;
            return response;

        }

        public async Task<Response<Order>> GetByIdAsync(string orderId)
        {
            var response = new Response<Order>();
            
            
                var exists = await _unitOfWork.OrderRepository.ExistsByIdAsync(orderId);

                if (!exists)
                {
                    response.Report.Add(Report.Create($"Order {orderId} doesn't exist!"));
                    return response;
                }

                var data = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);

                
                data.Items = await _unitOfWork.OrderRepository.ListItemByOrderIdAsync(orderId);


                response.Data = data;
                return response;

        }

        public async Task<Response> CreateAsync(Order order)
        {
            var response = new Response();
            _unitOfWork.BeginTransaction();
            try
            {
                var validation = new OrderValidation();

                var errors = validation.Validate(order).GetErrors();

                if (errors.Report.Count > 0)
                    return errors;

                
                order.OrderNumber = Guid.NewGuid().ToString("N");
                order.Id = Guid.NewGuid().ToString("N");
                order.OrderDate = DateTime.UtcNow;
                

                await _unitOfWork.OrderRepository.CreateAsync(order);

                _unitOfWork.CommitTransaction();
                return response;
            }
            catch(Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                return response;
            }
        }
    }
}
