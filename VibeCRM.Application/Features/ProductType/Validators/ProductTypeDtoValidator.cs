using FluentValidation;
using VibeCRM.Shared.DTOs.ProductType;

namespace VibeCRM.Application.Features.ProductType.Validators
{
    /// <summary>
    /// Validator for the ProductTypeDto class.
    /// Defines validation rules for product type data.
    /// </summary>
    public class ProductTypeDtoValidator : AbstractValidator<ProductTypeDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductTypeDtoValidator"/> class.
        /// Configures validation rules for ProductTypeDto properties.
        /// </summary>
        public ProductTypeDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Product type ID is required.");

            RuleFor(x => x.Type)
                .NotEmpty()
                .WithMessage("Product type name is required.")
                .MaximumLength(50)
                .WithMessage("Product type name cannot exceed 50 characters.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .MaximumLength(500)
                .WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Ordinal position must be a non-negative number.");
        }
    }
}