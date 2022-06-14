using FluentValidation;
using ProductDomain.Entities;

namespace ProductDomain.Validators
{
    public class ProductValidators : AbstractValidator<Product>
    {
        public ProductValidators()
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required....");

            RuleFor(x => x.ProductCategory)
                   .NotNull().WithMessage("Operation is required....")
                   .IsInEnum().WithMessage("Invalid Category....");

            RuleFor(x => x.GTIN)
                  .NotEmpty().WithMessage("GTIN is required....");
        }

    }
}
