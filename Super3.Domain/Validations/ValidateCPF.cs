using DocumentValidator;
using Super3.Domain.Model;

namespace Super3.Domain.Validations
{
    public class ValidateCPF
    {
        public string CpfValidate(Customer customer)
        {

            if (!CpfValidation.Validate(customer.Document))
                return false + "INVALID CPF";
            else
            {
                return true + "VALID CPF";
            }
               
        }
    }

    
        
}
