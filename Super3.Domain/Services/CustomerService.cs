using DocumentValidator;
using Super3.Domain.Interfaces.Repositories;
using Super3.Domain.Interfaces.Services;
using Super3.Domain.Model;
using Super3.Domain.Services;
using Super3.Domain.Validations;
using Super3.Domain.Validations.Base;
using System.Reflection.Metadata.Ecma335;

namespace Super3.Domain.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        
        async Task<Response<List<Customer>>> ICustomerService.GetAllAsync()
        {
            var response = new Response<List<Customer>>();

            var data = await _customerRepository.GetAllAsync();
            response.Data = data;
            return response;
        }
        async Task<Response<Customer>> ICustomerService.GetByIdAsync(int customerId)
        {
            var response = new Response<Customer>();

           
            var exists = await _customerRepository.ExistsByIdAsync(customerId);

            if (!exists)
            {
                response.Report.Add(Report.Create($"Customer {customerId} doesn't exist!"));
                return response;
            }
            //var customerIdStr = customerId.ToString();
            var data = await _customerRepository.GetByIdAsync(customerId);
            response.Data = data;
            return response;
        }
        async Task<Response> ICustomerService.CreateAsync(Customer customer)
        {
            var response = new Response();
            await ViaCepService.GetCepInfo(customer);
            var validation = new CustomerValidation();
            var errors = validation.Validate(customer).GetErrors();
            var validatecpf = (!CpfValidation.Validate(customer.Document));
            if(!validatecpf) return errors;
            if (errors.Report.Count > 0) return errors;

            



            await _customerRepository.CreateAsync(customer);

            return response;
        }
        async Task<Response> ICustomerService.UpdateAsync(Customer customer)
        {
            var response = new Response();
            await ViaCepService.GetCepInfo(customer);
            var validation = new CustomerValidation();
            var errors = validation.Validate(customer).GetErrors();

            if (errors.Report.Count > 0) return errors;

            
            var exists = await _customerRepository.ExistsByIdAsync(customer.Id);
            if (!exists)
            {
                response.Report.Add(Report.Create($"Customer {customer.Id} doesn't exist!"));
                return response;
            }
            if (!CpfValidation.Validate(customer.Document))
                return errors;

            

            await _customerRepository.UpdateAsync(customer);

            return response;
        }
    }
}





