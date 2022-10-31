using FluentValidation;
using Super3.Domain.Model;

namespace Super3.Domain.Validations
{
    public class StockValidation : AbstractValidator<Stock>
    {
        public StockValidation()
        {

            ValidatorOptions.Global.CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Quantity).NotNull().NotEmpty().WithMessage("Please add at least one value!");
            RuleFor(x => x.Quantity).LessThan(1).WithMessage("Please add at least one value!");
            RuleFor(x => x.Product.Id).NotNull().NotEmpty().WithMessage("Please add a product!");
        }
    }
}
