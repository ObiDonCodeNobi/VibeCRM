using FluentValidation;
using VibeCRM.Application.Features.PhoneType.DTOs;

namespace VibeCRM.Application.Features.PhoneType.Validators
{
    /// <summary>
    /// Validator for the PhoneTypeListDto class.
    /// Defines validation rules for phone type list properties.
    /// </summary>
    public class PhoneTypeListDtoValidator : AbstractValidator<PhoneTypeListDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneTypeListDtoValidator"/> class.
        /// Configures validation rules for PhoneTypeListDto properties.
        /// </summary>
        public PhoneTypeListDtoValidator()
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
        }
    }
}
