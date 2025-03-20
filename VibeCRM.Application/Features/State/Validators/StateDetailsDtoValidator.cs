using FluentValidation;
using VibeCRM.Application.Features.State.DTOs;

namespace VibeCRM.Application.Features.State.Validators
{
    /// <summary>
    /// Validator for the StateDetailsDto class.
    /// Defines validation rules for detailed state data including audit fields.
    /// </summary>
    public class StateDetailsDtoValidator : AbstractValidator<StateDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateDetailsDtoValidator"/> class.
        /// Configures validation rules for the StateDetailsDto properties.
        /// </summary>
        public StateDetailsDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("State ID is required.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("State name is required.")
                .MaximumLength(100)
                .WithMessage("State name cannot exceed 100 characters.");

            RuleFor(x => x.Abbreviation)
                .NotEmpty()
                .WithMessage("State abbreviation is required.")
                .MaximumLength(10)
                .WithMessage("State abbreviation cannot exceed 10 characters.");

            RuleFor(x => x.CountryCode)
                .NotEmpty()
                .WithMessage("Country code is required.")
                .MaximumLength(2)
                .WithMessage("Country code should be 2 characters.");

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
