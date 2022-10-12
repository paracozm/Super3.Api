using AutoMapper;
using Super3.Application.DataContract.Request.Order;
using Super3.Application.DataContract.Response.Customer;
using Super3.Application.DataContract.Response.Order;
using Super3.Application.Interfaces;
using Super3.Domain.Interfaces.Services;
using Super3.Domain.Model;
using Super3.Domain.Services;
using Super3.Domain.Validations.Base;

namespace Super3.Application.Applications
{
    public class OrderApplication : IOrderApplication
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderApplication(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        public async Task<Response<List<OrderResponse>>> GetAllAsync()
        {
            Response<List<Order>> order = await _orderService.GetAllAsync();

            if (order.Report.Any())
                return Response.Unprocessable<List<OrderResponse>>(order.Report);

            var response = _mapper.Map<List<OrderResponse>>(order.Data);

            return Response.OK(response);
        }

        public async Task<Response<OrderResponse>> GetByIdAsync(int orderId)
        {
            Response<Order> order = await _orderService.GetByIdAsync(orderId);

            if (order.Report.Any())
                return Response.Unprocessable<OrderResponse>(order.Report);

            var response = _mapper.Map<OrderResponse>(order.Data);

            return Response.OK(response);
        }

        public async Task<Response> CreateAsync(CreateOrderRequest order)
        {
            try
            {
                var orderModel = _mapper.Map<Order>(order);

                return await _orderService.CreateAsync(orderModel);
            }
            catch (Exception ex)
            {
                var response = Report.Create(ex.Message);
                return Response.Unprocessable(response);
            }
        }
    }
}
