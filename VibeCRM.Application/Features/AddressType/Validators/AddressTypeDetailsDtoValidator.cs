using FluentValidation;
using VibeCRM.Application.Features.AddressType.DTOs;

namespace VibeCRM.Application.Features.AddressType.Validators
{
    /// <summary>
    /// Validator for the AddressTypeDetailsDto class.
    /// Defines validation rules for detailed address type properties.
    /// </summary>
    public class AddressTypeDetailsDtoValidator : AbstractValidator<AddressTypeDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddressTypeDetailsDtoValidator"/> class.
        /// Configures validation rules for AddressTypeDetailsDto properties.
        /// </summary>
        public AddressTypeDetailsDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Address type ID is required.");

            RuleFor(x => x.Type)
                .NotEmpty()
                .WithMessage("Address type name is required.")
                .MaximumLength(50)
                .WithMessage("Address type name cannot exceed 50 characters.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .MaximumLength(500)
                .WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Ordinal position must be a non-negative number.");

            RuleFor(x => x.AddressCount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Address count must be a non-negative number.");

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