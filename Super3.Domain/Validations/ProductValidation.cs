using FluentValidation;
using Super3.Domain.Model;

namespace Super3.Domain.Validations
{
    public class ProductValidation : AbstractValidator<Product>
    {
    
        
        
            public ProductValidation()
            {
                RuleFor(x => x.ProductName).NotNull().NotEmpty().WithMessage("Product must have a name");
            }
        
    }
}
