using DocumentValidator;
using Super3.Domain.Model;

namespace Super3.Domain.Services
{
    public static class CPFValidationService
    {
        public static async Task<bool>CPFCheck(Customer customer)
        {
            var errorMessage = new string ("Invalid CPF");
            if (!CpfValidation.Validate(customer.Document))
            {
                throw new Exception(errorMessage);
            }
            else
            {
                 return true;
            }
        }
    }
}




