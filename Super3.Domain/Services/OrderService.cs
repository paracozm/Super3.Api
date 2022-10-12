﻿using Super3.Domain.Interfaces.Repositories;
using Super3.Domain.Interfaces.Services;
using Super3.Domain.Model;
using Super3.Domain.Validations;
using Super3.Domain.Validations.Base;

namespace Super3.Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<List<Order>>> GetAllAsync()
        {
            var response = new Response<List<Order>>();
            var data = await _unitOfWork.OrderRepository.GetAllAsync();
            response.Data = data;
            return response;
        }

        public async Task<Response<Order>> GetByIdAsync(int orderId)
        {
            var response = new Response<Order>();
            var exists = await _unitOfWork.OrderRepository.ExistsByIdAsync(orderId);

            if (!exists)
            {
                response.Report.Add(Report.Create($"Order {orderId} doesn't exist!"));
                return response;
            }

            var data = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
            response.Data = data;
            return response;
        }

        public async Task<Response> CreateAsync(Order order)
        {
            var response = new Response();
            var validation = new OrderValidation();


            var errors = validation.Validate(order).GetErrors();

            if (errors.Report.Count > 0)
                return errors;

            await _unitOfWork.OrderRepository.CreateAsync(order);

            return response;
        }
    }
}
