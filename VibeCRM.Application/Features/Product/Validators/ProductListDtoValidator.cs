using FluentValidation;
using VibeCRM.Shared.DTOs.Product;

namespace VibeCRM.Application.Features.Product.Validators
{
    /// <summary>
    /// Validator for the ProductListDto class
    /// </summary>
    public class ProductListDtoValidator : AbstractValidator<ProductListDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductListDtoValidator"/> class with validation rules.
        /// </summary>
        public ProductListDtoValidator()
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