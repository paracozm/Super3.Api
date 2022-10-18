using FluentValidation;
using Super3.Domain.Model;


namespace Super3.Domain.Validations
{
    public class OrderValidation : AbstractValidator<Order>
    {
        public OrderValidation()
        {

            ValidatorOptions.Global.CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Customer.Id).NotNull().WithMessage("Customer can't be null");
            RuleFor(X => X.Items).NotNull().WithMessage("Must have at least one item");
            //RuleFor(x => x.Items).NotNull().NotEmpty().WithMessage("Product Item can't be null");
            //RuleFor(x => x.Items.Count).NotEqual(0).WithMessage("Must have at least one item");

        }
    }
}
