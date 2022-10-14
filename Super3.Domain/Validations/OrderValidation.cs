using FluentValidation;
using Super3.Domain.Model;


namespace Super3.Domain.Validations
{
    public class OrderValidation : AbstractValidator<Order>
    {
        public OrderValidation()
        {
            //RuleFor(x => x.Customer.Id).NotNull().NotEmpty().WithMessage("Customer can't be null");
            //RuleFor(X => X.Product.Id).NotNull().NotEmpty().WithMessage("Product can't be null");
            //RuleFor(x=>x.Item.Id).NotNull().NotEmpty().WithMessage("Product Item can't be null");

        }
    }
}
