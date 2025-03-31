using FluentValidation;
using VibeCRM.Shared.DTOs.Product;

namespace VibeCRM.Application.Features.Product.Validators
{
    /// <summary>
    /// Validator for the ProductDto class
    /// </summary>
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDtoValidator"/> class with validation rules.
        /// </summary>
        public ProductDtoValidator()
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
        }
    }
}