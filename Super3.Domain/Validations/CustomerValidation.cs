using FluentValidation;
using Super3.Domain.Model;

namespace Super3.Domain.Validations
{
    public class CustomerValidation : AbstractValidator<Customer>
    {

        public CustomerValidation()
        {

            RuleFor(x => x.FirstName).NotNull().NotEmpty().Length(3, 30).WithMessage("Please fill in the first name field correctly!");
            RuleFor(x => x.LastName).NotNull().NotEmpty().Length(3, 30).WithMessage("Please fill in the last name field correctly!");
            RuleFor(x => x.Document).NotNull().NotEmpty().Length(11, 11).WithMessage("Please fill in the CPF field correctly!");
            RuleFor(x => x.CEP).NotNull().NotEmpty().Length(8, 8).WithMessage("Please fill in the CEP field correctly!");
            RuleFor(x => x.AddressNumber).NotNull().NotEmpty().Length(1, 6).WithMessage("Please fill in a valid address number!");


        }



    }
}
