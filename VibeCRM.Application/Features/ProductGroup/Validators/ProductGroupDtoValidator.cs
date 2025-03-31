using FluentValidation;
using VibeCRM.Shared.DTOs.ProductGroup;

namespace VibeCRM.Application.Features.ProductGroup.Validators
{
    /// <summary>
    /// Validator for the <see cref="ProductGroupDto"/> class.
    /// Defines validation rules for product group data.
    /// </summary>
    public class ProductGroupDtoValidator : AbstractValidator<ProductGroupDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductGroupDtoValidator"/> class.
        /// Sets up validation rules for product group data.
        /// </summary>
        public ProductGroupDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product group name is required.")
                .MaximumLength(100).WithMessage("Product group name cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Product group description is required.")
                .MaximumLength(500).WithMessage("Product group description cannot exceed 500 characters.");

            RuleFor(x => x.DisplayOrder)
                .GreaterThanOrEqualTo(0).WithMessage("Display order must be a non-negative number.");
        }
    }
}