using FluentValidation;
using VibeCRM.Application.Features.PhoneType.DTOs;

namespace VibeCRM.Application.Features.PhoneType.Validators
{
    /// <summary>
    /// Validator for the PhoneTypeDetailsDto class.
    /// Defines validation rules for detailed phone type properties.
    /// </summary>
    public class PhoneTypeDetailsDtoValidator : AbstractValidator<PhoneTypeDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneTypeDetailsDtoValidator"/> class.
        /// Configures validation rules for PhoneTypeDetailsDto properties.
        /// </summary>
        public PhoneTypeDetailsDtoValidator()
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

            RuleFor(x => x.PhoneCount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Phone count must be a non-negative number.");

            RuleFor(x => x.CreatedDate)
                .NotEmpty()
                .WithMessage("Created date is required.");

            RuleFor(x => x.CreatedBy)
                .NotEmpty()
                .WithMessage("Created by is required.");

            RuleFor(x => x.ModifiedDate)
                .NotEmpty()
                .WithMessage("Modified date is required.");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty()
                .WithMessage("Modified by is required.");
        }
    }
}