using FluentValidation;
using VibeCRM.Application.Features.State.DTOs;

namespace VibeCRM.Application.Features.State.Validators
{
    /// <summary>
    /// Validator for the StateDto class.
    /// Defines validation rules for basic state data.
    /// </summary>
    public class StateDtoValidator : AbstractValidator<StateDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateDtoValidator"/> class.
        /// Configures validation rules for the StateDto properties.
        /// </summary>
        public StateDtoValidator()
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
        }
    }
}
