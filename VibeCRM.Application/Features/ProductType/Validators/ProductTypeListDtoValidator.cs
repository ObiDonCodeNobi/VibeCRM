using FluentValidation;
using VibeCRM.Application.Features.ProductType.DTOs;

namespace VibeCRM.Application.Features.ProductType.Validators
{
    /// <summary>
    /// Validator for the ProductTypeListDto class.
    /// Defines validation rules for product type list data.
    /// </summary>
    public class ProductTypeListDtoValidator : AbstractValidator<ProductTypeListDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductTypeListDtoValidator"/> class.
        /// Configures validation rules for ProductTypeListDto properties.
        /// </summary>
        public ProductTypeListDtoValidator()
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
        }
    }
}