using FluentValidation;
using VibeCRM.Shared.DTOs.State;

namespace VibeCRM.Application.Features.State.Validators
{
    /// <summary>
    /// Validator for the StateListDto class.
    /// Defines validation rules for state list data.
    /// </summary>
    public class StateListDtoValidator : AbstractValidator<StateListDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateListDtoValidator"/> class.
        /// Configures validation rules for the StateListDto properties.
        /// </summary>
        public StateListDtoValidator()
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
        }
    }
}