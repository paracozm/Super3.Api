 using Super3.Domain.Interfaces.Repositories;
using Super3.Domain.Interfaces.Services;
using Super3.Domain.Model;
using Super3.Domain.Validations;
using Super3.Domain.Validations.Base;

namespace Super3.Domain.Services
{
    public class CustomerService : ICustomerService
    {
        
        
        private readonly IUnitOfWork _unitOfWork;

        
        public CustomerService(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;

        }


        public async Task<Response<List<Customer>>> GetAllAsync()
        {
            var response = new Response<List<Customer>>();
            

            var data = await _unitOfWork.CustomerRepository.GetAllAsync();
            response.Data = data;
            return response;
        }
        public async Task<Response<Customer>> GetByIdAsync(int customerId)
        {
            var response = new Response<Customer>();

           
            var exists = await _unitOfWork.CustomerRepository.ExistsByIdAsync(customerId);

            if (!exists)
            {
                response.Report.Add(Report.Create($"Customer {customerId} doesn't exist!"));
                return response;
            }
            var data = await _unitOfWork.CustomerRepository.GetByIdAsync(customerId);
            response.Data = data;
            return response;
        }
        public async Task<Response> CreateAsync(Customer customer)
        {
            var response = new Response();

            var validation = new CustomerValidation();

            await ViaCepService.GetCepInfo(customer);
            await CPFValidationService.CPFCheck(customer);

            var exists = await _unitOfWork.CustomerRepository.CpfExists(customer.Document.Replace(".", "").Replace("-", ""));
            if (exists)
            {
                response.Report.Add(Report.Create($"CPF: {customer.Document.Replace(".", "").Replace("-", "")} is already registered!"));
                return response;
            }

            var errors = validation.Validate(customer).GetErrors();

            if (errors.Report.Count > 0)
            {
                return errors;
            }

            await _unitOfWork.CustomerRepository.CreateAsync(customer);



            return response;
        }
    }
}





