using FluentValidation;
using VibeCRM.Application.Features.ProductGroup.DTOs;

namespace VibeCRM.Application.Features.ProductGroup.Validators
{
    /// <summary>
    /// Validator for the <see cref="ProductGroupDetailsDto"/> class.
    /// Defines validation rules for detailed product group data.
    /// </summary>
    public class ProductGroupDetailsDtoValidator : AbstractValidator<ProductGroupDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductGroupDetailsDtoValidator"/> class.
        /// Sets up validation rules for detailed product group data.
        /// </summary>
        public ProductGroupDetailsDtoValidator()
        {
            // Include validation rules from the base validator
            Include(new ProductGroupDtoValidator());

            // Additional validation rules specific to ProductGroupDetailsDto
            RuleFor(x => x.CreatedDate)
                .NotEmpty().WithMessage("Creation date is required.");

            RuleFor(x => x.ModifiedDate)
                .NotEmpty().WithMessage("Modification date is required.");

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("Creator information is required.");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty().WithMessage("Modifier information is required.");
        }
    }
}