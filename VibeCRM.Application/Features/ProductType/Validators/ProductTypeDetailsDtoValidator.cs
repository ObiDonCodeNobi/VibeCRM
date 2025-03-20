using FluentValidation;
using VibeCRM.Application.Features.ProductType.DTOs;

namespace VibeCRM.Application.Features.ProductType.Validators
{
    /// <summary>
    /// Validator for the ProductTypeDetailsDto class.
    /// Defines validation rules for detailed product type data.
    /// </summary>
    public class ProductTypeDetailsDtoValidator : AbstractValidator<ProductTypeDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductTypeDetailsDtoValidator"/> class.
        /// Configures validation rules for ProductTypeDetailsDto properties.
        /// </summary>
        public ProductTypeDetailsDtoValidator()
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

            RuleFor(x => x.ProductCount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Product count must be a non-negative number.");

            RuleFor(x => x.CreatedDate)
                .NotEmpty()
                .WithMessage("Created date is required.");

            RuleFor(x => x.CreatedBy)
                .NotEmpty()
                .WithMessage("Created by user ID is required.");

            RuleFor(x => x.ModifiedDate)
                .NotEmpty()
                .WithMessage("Modified date is required.");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty()
                .WithMessage("Modified by user ID is required.");
        }
    }
}