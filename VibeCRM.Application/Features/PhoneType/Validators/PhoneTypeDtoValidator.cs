using FluentValidation;
using VibeCRM.Shared.DTOs.PhoneType;

namespace VibeCRM.Application.Features.PhoneType.Validators
{
    /// <summary>
    /// Validator for the PhoneTypeDto class.
    /// Defines validation rules for phone type properties.
    /// </summary>
    public class PhoneTypeDtoValidator : AbstractValidator<PhoneTypeDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneTypeDtoValidator"/> class.
        /// Configures validation rules for PhoneTypeDto properties.
        /// </summary>
        public PhoneTypeDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Phone type ID is required.");

            RuleFor(x => x.Type)
                .NotEmpty()
                .WithMessage("Phone type name is required.")
                .MaximumLength(50)
                .WithMessage("Phone type name cannot exceed 50 characters.");

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