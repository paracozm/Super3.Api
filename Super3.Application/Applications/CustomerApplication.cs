using AutoMapper;
using Super3.Application.DataContract.Request.Customer;
using Super3.Application.DataContract.Response.Customer;
using Super3.Application.Interfaces;
using Super3.Domain.Interfaces.Services;
using Super3.Domain.Model;
using Super3.Domain.Validations.Base;

namespace Super3.Application.Applications
{
    public class CustomerApplication : ICustomerApplication
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerApplication(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }
        public async Task<Response<List<CustomerResponse>>> GetAllAsync()
        {
            Response<List<Customer>> customer = await _customerService.GetAllAsync();

            if (customer.Report.Any())
                return Response.Unprocessable<List<CustomerResponse>>(customer.Report);

            var response = _mapper.Map<List<CustomerResponse>>(customer.Data);

            return Response.OK(response);
        }



        public async Task<Response<CustomerResponse>> GetByIdAsync(int customerId)
        {
            Response<Customer> customer = await _customerService.GetByIdAsync(customerId);

            if (customer.Report.Any())
                return Response.Unprocessable<CustomerResponse>(customer.Report);

            var response = _mapper.Map<CustomerResponse>(customer.Data);

            return Response.OK(response);
        }

        public async Task<Response> CreateAsync(CreateCustomerRequest customer)
        {

            try
            {
                var customerModel = _mapper.Map<Customer>(customer);
                var response = await _customerService.CreateAsync(customerModel);
                return response;
            }
            catch (Exception ex)
            {
                var response = Report.Create(ex.Message);

                return Response.Unprocessable(response);
            }
        }

    }
}
