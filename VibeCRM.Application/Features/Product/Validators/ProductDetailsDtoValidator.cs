using FluentValidation;
using VibeCRM.Shared.DTOs.Product;

namespace VibeCRM.Application.Features.Product.Validators
{
    /// <summary>
    /// Validator for the ProductDetailsDto class
    /// </summary>
    public class ProductDetailsDtoValidator : AbstractValidator<ProductDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDetailsDtoValidator"/> class with validation rules.
        /// </summary>
        public ProductDetailsDtoValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("Product ID is required.");

            RuleFor(p => p.ProductTypeId)
                .NotEmpty().WithMessage("Product Type ID is required.");

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");

            RuleFor(p => p.Description)
                .MaximumLength(500).WithMessage("Product description cannot exceed 500 characters.");

            // Validate collections if they are not null
            When(p => p.QuoteLineItems != null, () =>
            {
                RuleForEach(p => p.QuoteLineItems)
                    .SetValidator(new QuoteLineItem.Validators.QuoteLineItemDtoValidator());
            });

            When(p => p.SalesOrderLineItems != null, () =>
            {
                RuleForEach(p => p.SalesOrderLineItems)
                    .SetValidator(new SalesOrderLineItem.Validators.SalesOrderLineItemDtoValidator());
            });

            When(p => p.ProductGroups != null, () =>
            {
                RuleForEach(p => p.ProductGroups)
                    .SetValidator(new ProductGroup.Validators.ProductGroupListDtoValidator());
            });
        }
    }
}